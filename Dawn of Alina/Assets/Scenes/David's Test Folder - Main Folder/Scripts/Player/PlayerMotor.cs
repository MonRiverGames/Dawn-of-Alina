using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private InputManager inputManager;
    private PlayerInput playerInput;

    private Animator anim;
    
    private Vector3 playerVelocity;

    
    public float speed = 2f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    
    public float animationFinishTime = 0.9f;
    public float velocity = 0f;
    public float backVelocity = 0f;
    public float acceleration = 0.5f;
    public float attackCoolDown = 0.5f;

    public int selectedSpell;

    public bool isGrounded;
    public bool sprinting;
    public bool isWalking;
    public bool isAttacking;
    public bool canAttack;
    public bool walkingBackwards;
    public bool runningBackwards;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSpell();
        WalkForwards();
        WalkBackwards();
        PlayAttack();
        

        
    }
    
    //Recieve input from InputManager.cs and apply to character controller
    public void ProcessMove(Vector3 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.y = input.y;
        moveDir.z = input.z;
        controller.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        
        playerVelocity.y -= gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        
        controller.Move(playerVelocity * Time.deltaTime);

    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * 3f * gravity);
            anim.SetTrigger("Jump");
        }
    }

    public void WalkForwards()
    {
        if(!isWalking && !sprinting)
        {
            velocity = 0;
        }
        
        if (Keyboard.current.wKey.isPressed)
        {
            isWalking = true;
            velocity += Time.deltaTime * acceleration;
            if(velocity > 0.3 && !sprinting)
            {
                velocity = 0.3f;
            }


        }

        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            isWalking = false;
            sprinting = false;
            velocity = 0;
        }
        

        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && !walkingBackwards)
        {
            sprinting = true;

            velocity += Time.deltaTime * acceleration;

            


        }

        if (Keyboard.current.leftShiftKey.wasReleasedThisFrame && !runningBackwards)
        {
            
            sprinting = false;
            speed = 2f;


            velocity -= Time.deltaTime * acceleration;
            if (velocity < 0)
            {
                velocity = 0;
            }
        }

        if(velocity > 1)
        {
            velocity = 1;
        }

        if(sprinting){
            speed++;

            if (speed > 8)
            {
                speed = 8;
            }
        }
        

        anim.SetFloat("Velocity", velocity);
    }
    
    public void WalkBackwards()
    {
        if (!walkingBackwards && !runningBackwards)
        {
            backVelocity = 0;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            walkingBackwards = true;
            anim.SetBool("isWalkingBackwards", true);
            backVelocity += Time.deltaTime * acceleration;
            if (backVelocity > 0.3 && !sprinting)
            {
                backVelocity = 0.3f;
            }


        }

        if (Keyboard.current.sKey.wasReleasedThisFrame)
        {
            walkingBackwards = false;
            runningBackwards = false;
            anim.SetBool("isWalkingBackwards",false);
            backVelocity = 0;
        }


        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && walkingBackwards)
        {
            runningBackwards = true;

            backVelocity += Time.deltaTime * acceleration;




        }

        if (Keyboard.current.leftShiftKey.wasReleasedThisFrame && walkingBackwards)
        {
            runningBackwards = false;
            speed = 2f;


            backVelocity -= Time.deltaTime * acceleration;
            if (backVelocity < 0)
            {
                backVelocity = 0;
            }
        }

        if (backVelocity > 1)
        {
            backVelocity = 1;
        }

        if (runningBackwards)
        {
            speed++;

            if (speed > 8)
            {
                backVelocity = 1;
                speed = 8;
            }
        }


        anim.SetFloat("BackVelocity", backVelocity);
    }

    public void Attack(int selectedSpell)
    {
        

        
            if(selectedSpell == 1) 
            {
                anim.SetTrigger("MagicBeam");
            }
            
            if(selectedSpell == 2)
            {
                anim.SetTrigger("MagicClap");
            }

            if(selectedSpell == 3)
            {
                anim.SetTrigger("MagicSpiritFingers");
            }
        

        canAttack = false;
        StartCoroutine(AttackCoolDown());
        
        
    }
    
    public void PlayAttack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack(selectedSpell);
        }
    }

    public IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    public void SetSpell()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectedSpell = 1;
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectedSpell = 2;
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            selectedSpell = 3;
        }
    }
    

}
