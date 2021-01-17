using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour
{

    public float speed;//速度
    public float waitTime;//等待的时间
    public Transform[] movePos;

    private int i;
    private Transform playerDefTransfrom;

    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        playerDefTransfrom = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position,speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1)
        {
            if (waitTime < 0.0f)
            {
                if (i == 0)
                {
                    i = 1;
                }
                else {
                    i = 0;
                }

                waitTime = 0.5f;
            } else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

     void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = playerDefTransfrom;
        }
    }
}
