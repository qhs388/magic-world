using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//人物位置
    public float smoothing;//平滑的值

    public Vector2 minPostion;
    public Vector2 maxPostion;

    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
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
                    targetPos.x = Mathf.Clamp(targetPos.x, minPostion.x, maxPostion.x);
                    targetPos.y = Mathf.Clamp(targetPos.y, minPostion.y, maxPostion.y);

                    transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
                }
            }
        }
    }
    

    public void setCamPosLimit( Vector2 minPos,Vector2 maxPos)
    {
        minPostion = minPos;
        maxPostion = maxPos;


    }
}
