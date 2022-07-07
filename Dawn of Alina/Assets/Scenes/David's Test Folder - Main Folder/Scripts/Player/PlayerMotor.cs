using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;

    private Animator anim;
    
    private Vector3 playerVelocity;
    
    public float speed = 2f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crouchTimer;
    public float animationFinishTime = 0.9f;

    public bool isGrounded;
    public bool lerpCrouch;
    public bool crouching;
    public bool sprinting;
    public bool isWalking;
    public bool isAttacking;
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessCrouch();
        ProcessAttack();
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

        AnimateWalk(moveDir);
        Debug.Log(moveDir);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * 3f * gravity);
        }
    }
    
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    
    public void ProcessCrouch()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1f, p);

            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2f, p);

            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    
    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 2f;
        }
    }

    public void AnimateWalk(Vector3 input)
    {
        //isWalking = (input.x > 0.1f || input.x < 0.1f) || (input.z > 0.1f || input.z < 0.1f) ? true : false;

        if (input.x > 0.1f || input.z > 0.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
    }
    
    public void Attack()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("isAttacking");
            StartCoroutine(AttackCoolDown());
        }
    }
    
    public void ProcessAttack()
    {
        if (isAttacking && anim.GetCurrentAnimatorStateInfo(1).normalizedTime >= animationFinishTime)
        {
            isAttacking = false;
        }
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacking = true;
        
    }


}
