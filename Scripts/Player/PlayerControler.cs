using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
   
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Animator Anim;
    public bool isGround = true;//默认在地面上
    private float JumpSpeed = 0;

   
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        turn(moveX);
        Jump(moveY);
        Movement(moveX);

    }
    public void  Movement( float moveX)
    {
        // 移动人物
        rb.velocity = new Vector2(moveX * moveSpeed , rb.velocity.y);
        Anim.SetFloat("Speed",math.abs(moveX));//math.abs//绝对值，用来控制动画条件

    }

    public void Jump( float moveY)
    {
        if (isGround)
        {
          JumpSpeed = -jumpForce;
          // 跳跃,有问题
          if (Input.GetButtonDown("Jump") && isGround == true)
          {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            JumpSpeed = jumpForce * Time.deltaTime;
            isGround = false;
            Anim.SetTrigger("Jump");
            Anim.SetFloat("VariticalSpeed", JumpSpeed);
          }
            else if(!Input.GetButtonDown("Jump") && JumpSpeed > 0)
            {
                JumpSpeed -= jumpForce * Time.deltaTime;
            }
        }
        if(!isGround) 
        {
            JumpSpeed -= jumpForce * Time.deltaTime;
            Anim.SetFloat("VariticalSpeed", JumpSpeed);
        }
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    public void turn(float moveX)
    {
        
        // 改变人物朝向和翻转
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

    }
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
