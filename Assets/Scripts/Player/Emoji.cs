using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour
{

    public GameObject emoji;
    public float speed;
    public float time;//表情展示时间
    private GameObject child;
    float ColorAlpha = 1f;//透明程度
    private bool showBox;
    

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (showBox)
            {
                DestroyEmoji();
            }

            child = Instantiate(emoji);

            child.transform.parent = transform;

            showBox = true;
            Invoke("DestroyEmoji", time);
        }
        
        if (showBox==true)
        {
           // Alpha();

        }
    }

    void DestroyEmoji()
    {
        Destroy(child);
    }



   void Alpha()
    {
   
        if (ColorAlpha >0)
        {
            print(child);
            ColorAlpha -= Time.deltaTime / 2;

            //child.transform.GetComponent<SpriteRenderer>().flipY = true;
            child.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            showBox = false;
           
        }
    }
}
