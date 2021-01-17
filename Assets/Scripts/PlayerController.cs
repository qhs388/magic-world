using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float restoreTime;//下落恢复的时间

    public float climbSpeed;//爬梯子的速度

    private Rigidbody2D myRigidbody;
    private Animator myAnim;

    private BoxCollider2D myFeet;

    private bool isGround;
    private bool canDoubleJump;
    private bool isOneWayPlatform;

    private bool isLadder;//判断是不是梯子
    private bool isClimbing;//判断是不是正在爬梯子

    private bool isJumping;//是不是在跳跃
    private bool isFalling;//是不是掉落

    private bool isDoubleJumping;//是不是在二段跳跃
    private bool isDoubleFalling;//是不是二段掉落

    private float playerGravity;//人物的重力

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;//获取当前的重力

    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.isGameAlive)
        {
            Run();
            Flip();
            Jump();
            Climb();

            CheckAirStatus();//检测是不是在空中

            CheckGround();
            SwitchAnimation();
            OneWayPlatformChek();
            CheckLadder();
            //Attack();
        }


    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("MoveingPlatform"))||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));


        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    void Flip()//奔跑的时候左右翻转
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVel;

        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnim.SetBool("Running", playerHasXAxisSpeed);


    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);

                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);

                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);

                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
           
        }
    }


    void Climb()//爬梯子功能
    {
        
        if (isLadder)//判断是不是已经跟梯子接触了
        {
            
            float moveY = Input.GetAxis("Vertical");
          
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("Climbing", true);//播放爬梯子的动画
                myRigidbody.gravityScale = 0.0f;//人物重力设置为0
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);
            }
            else 
            {

               
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)//判断如果是路过并没有爬梯子
                {
                    myAnim.SetBool("Climbing", false);//就不播放动画

                }
                else
                {
                    myAnim.SetBool("Climbing", false);//就不播放动画
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else{//如果不在梯子
            myAnim.SetBool("Climbing", false);//就不播放动画
            myRigidbody.gravityScale = playerGravity;//人物重力设置为0
        }


    }

    //void Attack()//普通攻击
    //{
    //    print(Input.GetButtonDown("Attack"));
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        print(Input.GetButtonDown("Attack"));
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    void SwitchAnimation()//动画状态的切换
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void OneWayPlatformChek()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckAirStatus() { //检测是不是在空中

        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");

    }

}
