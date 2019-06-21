using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_control : MonoBehaviour
{
    private Transform playerTrans;

    public bool follow;
    public bool follow_x;
    public bool follow_y;
    public float detect_val=10;
    public float x_border_right;
    public float x_border_left;
    public float y_border_up;
    public float y_border_down;

    public float speed;

    void Start()
    {

    }

    void Update()
    {
        if (playerTrans == null)//对象被销毁
            playerTrans = GameObject.FindWithTag("note").transform;
        //移动线性插值
        else if (playerTrans != null)
        {
            if (follow)
            {
                Vector3 targetPos = playerTrans.position + new Vector3(0, 2.4f, -2.4f);
                if (Mathf.Abs(this.transform.position.x - targetPos.x) >= detect_val || (this.transform.position.y - targetPos.y) >=detect_val)
                    this.transform.position = Vector3.Slerp(this.transform.position, targetPos, speed * Time.deltaTime);
            }
            else if(follow_x)
            {
                Vector3 targetPos = playerTrans.position;
                if (Mathf.Abs(this.transform.position.x - targetPos.x) >= detect_val&&this.transform.position.x <x_border_right && targetPos.x > x_border_left)
                    this.transform.Translate(new Vector3((targetPos.x- this.transform.position.x ) * Time.deltaTime *speed, 0), Space.Self);
            }
            else if(follow_y)
            {
                Vector3 targetPos = playerTrans.position;
                if (Mathf.Abs(this.transform.position.y - targetPos.y) >= detect_val && this.transform.position.y < y_border_up && targetPos.y > y_border_down)
                    this.transform.Translate(new Vector3(0,(targetPos.y - this.transform.position.y) * Time.deltaTime * speed), Space.Self);
            }
        }
    }
}
