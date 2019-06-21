using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour
{
    public bool is_appear_suddenly;//判断是否突然初始化
    public bool is_move = false;//判断是否移动
    public bool is_trigger_on_x = true;//true表示为x轴触发
    public bool is_move_trigger_in_yuan = false;//move是否为圆触发
    public bool is_appear_trigger_in_yuan = false;//appear是否为圆触发
    public float trigger_r;//圆触发半径

    private bool is_start_move_y = false;//是否开始移动
    private bool is_start_move_x = false;//是否开始移动
    public bool is_on_speed = false;//判断是否加速移动
    public float speed_increse;//加速度大小 

    public float is_trigger_x;//移动触发位置横坐标
    public float is_trigger_y;//移动触发位置纵坐标
    public float move_speed;//移动速度

    public bool if_reborn;//判断是否会重生(move的)
    public bool reborn_withnote;
    public float reborn_x;//重生边界
    public float reborn_y;//重生边界
    private Vector3 reborn_place;//重生点坐标

    GameObject note;
    public GameObject barrier_tobeappear;

    private void Start()
    {
        reborn_place = this.gameObject.transform.position;
    }
    void Update()
    {
       // Debug.LogFormat("{0}", reborn_place);
        if (if_reborn)
        {
            if (reborn_withnote)
            {
                if(Scene_manager.Instance.is_dead )
                {
                    this.gameObject.transform.position = reborn_place;
                    is_start_move_x = false;
                    is_start_move_y = false;
                }
            }
            else
            {
                if (reborn_y != 0 && Mathf.Abs(this.gameObject.transform.position.y - reborn_y) < 1)
                {
                    this.gameObject.transform.position = reborn_place;
                    is_start_move_x = false;
                    is_start_move_y = false;
                }
                else if (reborn_x != 0 && Mathf.Abs(this.gameObject.transform.position.x - reborn_x) < 1)
                {
                    this.gameObject.transform.position = reborn_place;
                    is_start_move_x = false;
                    is_start_move_y = false;
                }
            }
            
        }

        if (note == null)
        {
            note = GameObject.FindWithTag("note");
        }
        else if (note != null)
        {
            if (!is_move)
            {
                if (is_appear_suddenly)
                {
                    if (is_appear_trigger_in_yuan)
                    {
                        //Debug.Log("000");
                        if (((note.transform.position.x - is_trigger_x) * (note.transform.position.x - is_trigger_x) + (note.transform.position.y - is_trigger_y) * (note.transform.position.y - is_trigger_y)) < trigger_r * trigger_r)
                            barrier_tobeappear.SetActive(true);
                        else
                            barrier_tobeappear.SetActive(false);

                    }
                    else if (!is_appear_trigger_in_yuan)
                    {
                        if (Mathf.Abs(note.transform.position.x - is_trigger_x) < 3.5 && is_trigger_on_x)
                        {
                            barrier_tobeappear.SetActive(true);
                        }
                        else if (Mathf.Abs(note.transform.position.y - is_trigger_y) < 3.5 && !is_trigger_on_x)
                        {
                            barrier_tobeappear.SetActive(true);
                        }
                     else
                        barrier_tobeappear.SetActive(false);
                    }
                   else
                        barrier_tobeappear.SetActive(false);
                }
            }
            else if (is_move)
            {
                if (!is_move_trigger_in_yuan)
                {
                    if ((Mathf.Abs(note.transform.position.x - is_trigger_x) < 0.1 && is_trigger_on_x) || is_start_move_x)
                    {
                        //Debug.Log("111");
                        this.transform.Translate(new Vector3(0, move_speed * Time.deltaTime), Space.Self);
                        if (is_on_speed)
                            move_speed += Time.deltaTime * speed_increse;
                        is_start_move_x = true;
                    }
                    else if ((Mathf.Abs(note.transform.position.y - is_trigger_y) < 0.1 && !is_trigger_on_x) || is_start_move_y)
                    {
                        this.transform.Translate(new Vector3(0, move_speed * Time.deltaTime), Space.Self);
                        is_start_move_y = true;
                    }
                }
                else if ((is_move_trigger_in_yuan && ((note.transform.position.x - is_trigger_x) * (note.transform.position.x - is_trigger_x) + (note.transform.position.y - is_trigger_y) * (note.transform.position.y - is_trigger_y) < trigger_r * trigger_r)) || is_start_move_x)
                {
                    this.transform.Translate(new Vector3(0, move_speed * Time.deltaTime), Space.Self);
                    is_start_move_x = true;
                }
            }

        }
    }
}
