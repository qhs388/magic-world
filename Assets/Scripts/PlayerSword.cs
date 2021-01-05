using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public int damage;//伤害值
    public float startTime;
    public float time;
    private Animator anim;
    private PolygonCollider2D collider2D;


    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
           
            
            anim.SetTrigger("Attack");
            StartCoroutine(StartSword());
        }
    }

    IEnumerator StartSword()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        StartCoroutine(disableHitBox());
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))//判断是不是碰撞到了敌人(Enemy)
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
