using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Blinks;//受伤闪烁的次数
    public float time;//受伤闪烁的时间
    public float dieTime;//死亡后多久删除主角
    public float hitBoxCdTime;//被攻击的延迟

    private Renderer myRenderer;
    private Animator anim;
    private ScreenFlash ScreenFlash;
    private Rigidbody2D rb2d;
    private PolygonCollider2D polygonCollider2D;

   

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.healthMax = health;
        HealthBar.healthCurrent = health;

        myRenderer = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        ScreenFlash = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        ScreenFlash.FlashScreen();

        if (health<0)
        {
            health = 0;
        }
        HealthBar.healthCurrent = health;
        if (health <= 0)
        {
            rb2d.velocity = new Vector2(0, 0);
           // rb2d.gravityScale = 0.0f;//
            GameController.isGameAlive = false;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);
        }
        BlinkPlayer(Blinks, time);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        polygonCollider2D.enabled = true;
    }

    void KillPlayer()
    {
        Destroy(gameObject);
    }

    void BlinkPlayer(int numBlinks,float seconds)
    {
        StartCoroutine(DoBlinks( numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)   
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRenderer.enabled = true;
    }
}
