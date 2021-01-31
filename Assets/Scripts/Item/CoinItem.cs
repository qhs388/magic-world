using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")//捡金币
        {
            SoundManager.PlayPickCoinClip();//播放捡金币的音效
            CoinUI.currentCoinQuantity += 1;
            Destroy(gameObject);


        }
    }
}
