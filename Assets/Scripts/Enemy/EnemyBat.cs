using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{

    public float speed;//速度
    public float startWaitTime;//等待时间
    private float waitTime;



    public Transform movePos; // 下一次移动的位置
    public Transform leftDownPos;//飞行的范围 (左下角)
    public Transform rightUpPos; //飞行的范围 (右上角)  


    // Start is called before the first frame update
    void Start()
    {
        //调用父类的Start
        base.Start();

        waitTime = startWaitTime;

        movePos.position = GetRandomPos();

    }

    // Update is called once per frame
    void Update()
    {
        //调用父类的Update
        base.Update();

        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);//移动

        if (Vector2.Distance(transform.position,movePos.position) < 0.1f)//判断是不是到达了位置
        {
            if (waitTime <= 0)
            {
                movePos.position = GetRandomPos();

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPos()//获取随机的位置
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));

        return rndPos;
    }
   
}
