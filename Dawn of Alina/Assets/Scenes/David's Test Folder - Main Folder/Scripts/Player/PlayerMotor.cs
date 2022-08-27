using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{
    public Image barImage;
    public float maxMana = 1f;
    public float currentMana;


    private CharacterController controller;
    private InputManager inputManager;
    private PlayerInput playerInput;
    public GameObject groundCheck;
    private Animator anim;
    public ParticleSystem magicClapFX;
    private Vector3 playerVelocity;

    
    public float speed = 2f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float groundCheckRay = 1;
    
    public float animationFinishTime = 0.9f;
    public float velocity = 0f;
    public float backVelocity = 0f;
    public float acceleration = 0.5f;
    public float attackCoolDown = 1f;
    public float attackRange = 4f;

    public int selectedSpell;

    public bool isGrounded = true;
    public bool sprinting;
    public bool isWalking;
    public bool isAttacking;
    public bool canAttack;
    public bool walkingBackwards;
    public bool runningBackwards;

    
    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        barImage.fillAmount = currentMana;
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
        GroundCheck();

        if(currentMana >= 1)
        {
            currentMana = 1;
        }

        
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
        

        if(currentMana >= .75f)
        {
            if (selectedSpell == 1)
            {
                anim.SetTrigger("MagicBeam");
                SpendMana(.5f);

            }

            if (selectedSpell == 2)
            {
                anim.SetTrigger("MagicClap");
                SpendMana(.75f);

            }

            if (selectedSpell == 3)
            {
                anim.SetTrigger("MagicSpiritFingers");
                SpendMana(.25f);

            }
        }
           
        canAttack = false;
        StartCoroutine(AttackCoolDown());
        StartCoroutine(FillMana());
        
        
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
    
    public void GroundCheck()
    {
        Ray ray = new Ray(groundCheck.transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.distance < 0.5f)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    public void SpendMana(float manaCost)
    {
        currentMana -= manaCost * Time.deltaTime;
        barImage.fillAmount = currentMana;
    }

    public void RegenMana(float manaRegen)
    {
        currentMana += manaRegen * Time.deltaTime;
        barImage.fillAmount = currentMana;
    }

    IEnumerator FillMana()
    {
        yield return new WaitForSeconds(3);
        RegenMana(.5f);
    }

    public void PlayMagicClapFX(){
        magicClapFX.Play();
        CheckForEnemies(100);
    }

    public void CheckForEnemies(int attackDamage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in hitColliders)
        {
            if (collider.GetComponentInParent<EnemyController>()) 
            {
                collider.GetComponentInParent<EnemyController>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }


}
