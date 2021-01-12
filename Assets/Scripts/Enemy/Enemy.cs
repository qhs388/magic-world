using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float flashTime;//受伤的闪烁时间
    public GameObject bloodEffect;//血液特效

    private SpriteRenderer sr;
    private Color originaColor;//最开始的颜色

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originaColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int PlayerDamage)
    {
        FlashColor(flashTime);
        health -= PlayerDamage;

        Instantiate(bloodEffect, transform.position, Quaternion.identity);//生成血液特效
        GameController.camShake.Shake();
    }

    void FlashColor(float time)
    {

        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originaColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        print(other.GetType().ToString());

        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//判断是不是碰撞到了主角(Player)
        {
            print("dasdsadsadsa");
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
            
        }
    }
}
