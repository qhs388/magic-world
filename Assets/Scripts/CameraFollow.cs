using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//人物位置
    public float smoothing;//平滑的值

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (target != null)//如果不判断人物死掉就要报错
        {
            if (target)
            {
                if (transform.position  != target.position)
                {
                    Vector3 targetPos = target.position;
                    transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
