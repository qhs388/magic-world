using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{

    public float speed;
    public int damage;
    public float rotateSpeed;
    public float tuning;

    private Rigidbody2D rb2d;
    private Transform playerTransfrom;//玩家位置
    private Transform sickleTransfrom;//回旋镖位置
    private Vector2 startSpeed;

    


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        startSpeed = rb2d.velocity;
        playerTransfrom = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransfrom = GetComponent<Transform>();



    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);//让回旋镖旋转

        float y = Mathf.Lerp(transform.position.y, playerTransfrom.position.y, tuning);

        transform.position = new Vector3(transform.position.x, y, 0);
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - playerTransfrom.position.x) <0.5f)
        {
            Destroy(gameObject);
        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))

        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
