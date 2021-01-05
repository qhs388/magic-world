using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private float rotateSpeed; //武器旋转的速度
    [SerializeField] private bool isRotaing;

    [SerializeField] private float moveSpeed;//武器速度
    [SerializeField] private int attackDistance = 3;//武器攻击距离

    private Vector3 targetPos;
    private bool isClicked;
    private bool isDamaged;//判断斧头是不是已经停下来

    private Transform playerTrans;//玩家的位置;
    private bool canCallBack;//值默认为false
    private bool returnWeapon;

    private Quaternion WeRotation;

    private void Start()
    {
        WeRotation = transform.rotation;

     

        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        SelfRotation(); 

        if (Input.GetKeyDown(KeyCode.Q) && isClicked == false)//当按Q的时候，设置斧头的丢出的位置
        {
            isClicked = true;
            targetPos = new Vector3(transform.position.x+attackDistance, 0, 0);
            playerTrans.DetachChildren();
        }

        if (returnWeapon)
        {
            BackWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q) && canCallBack)
        {
            gameObject.transform.parent = playerTrans;
            isDamaged = true;
            returnWeapon = true;


            print(playerTrans);


           


        }

        if (isClicked)//如果斧头丢出就移动到相应的位置
        {
            ThrowWeapon();
           
        }

        if (Vector2.Distance(transform.position, targetPos)<=0.01f)//如果斧头到达指定的位置就不旋转了
        {
            isRotaing = false;
            isDamaged = false;
            canCallBack = true;
        }

       

        if (Vector2.Distance(transform.position, playerTrans.position) <= 0.5f)
        {


            

            isRotaing = false;
            canCallBack = false;
            returnWeapon = false;
            isDamaged = false;
            isClicked = false;

            transform.rotation = WeRotation;
        }

    }
    private void BackWeapon()
    {
        isRotaing = true;
        transform.position = Vector2.MoveTowards(transform.position, playerTrans.position, moveSpeed* 5 * Time.deltaTime);

    }

    private void ThrowWeapon()
    {
      
        isRotaing = true;
       
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        isDamaged = true;
    }

    private void SelfRotation()//斧头旋转
    {
        if (isRotaing)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        }
        else{
            transform.Rotate(0, 0, 0);
        }
    }
}
