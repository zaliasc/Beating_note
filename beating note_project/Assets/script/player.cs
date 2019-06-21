using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class player: MonoBehaviour
{
    public Sprite[] noteList;
    public AudioClip[] audioclipList;
    private int note_num;
    Rigidbody2D rbody;
    SpriteRenderer sr;
    public AudioSource audio;
    public AudioSource jumo_audio;
    private int rotation_order = 0;//旋转方向，0为右，1为左
    private int rotation_num = 0;//旋转帧数
    private float rotation_v = 0;//旋转速度
    private int rotation_num_count = 0;//帧数计数
    private bool is_on_ground = false;   //是否在地面上
    private float jumpPressure = 0f;  //蓄力值
    //private float MinjumpPressure = 20f;  //蓄力最小值
    private float MaxjumpPressure = 30f;  // 蓄力最大值
    private float max_time = 0.09f;//蓄力最长时间
    //private float min_time = 0.05f;//蓄力最短时间
    private float counter_time = 0f;
    private bool double_jump = true;
    public float moveSpeed = 10;


    private void Awake()
    {
        is_on_ground = true;  //初始设置在地面上
        rbody = GetComponent<Rigidbody2D>();  //获取组件
        sr = GetComponent<SpriteRenderer>();
        if(Scene_manager.Instance.Scene_num == 0)
        {
            rotation_v = 1.0f;
            rotation_num = 30;
            note_num = 0;
        }
        else if (Scene_manager.Instance.Scene_num == 1)
        {
            rotation_v = 1.6f;
            rotation_num = 17;
            note_num = 0;
        }
        else if(Scene_manager.Instance.Scene_num == 2)
        {
            rotation_v = 1.7f;
            rotation_num = 15;
            note_num = 1;
        }
        else if (Scene_manager.Instance.Scene_num ==3)
        {
            rotation_v = 1.6f;
            rotation_num = 20;
            note_num = 2;
        }
        sr.sprite = noteList[note_num];
        //if (Scene_manager.Instance.Scene_num != 0)
        //{
        //    audio.clip = audioclipList[Scene_manager.Instance.Scene_num];
        //    audio.Play();
        //}
        //else
        //{
        //    audio.clip = audioclipList[0];
        //    audio.Play();
        //}
    }

    void Start()
    {
        transform.transform.Rotate(new Vector3(0, 0, (rotation_v * rotation_num / 2)));
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        if (rotation_order == 0)
        {
            transform.Rotate(new Vector3(0, 0, -rotation_v));
            rotation_num_count++;
            if (rotation_num_count == rotation_num)
            {
             //   Debug.LogFormat("{0}", rotation_num);
                rotation_num_count = 0;
                rotation_order = 1;
            }
        }
        else if (rotation_order == 1)
        {
            transform.Rotate(new Vector3(0, 0, rotation_v));
            rotation_num_count++;
            if (rotation_num_count == rotation_num)
            {
                rotation_num_count = 0;
                rotation_order = 0;
            }
        }
        Move();
        if(Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
            Scene_manager.Instance.is_dead = true;
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-1 * moveSpeed * Time.deltaTime, 0), Space.World);
        }

        if (Input.GetKey(KeyCode.RightArrow))

        {
            transform.Translate(new Vector3(1 * moveSpeed * Time.deltaTime, 0), Space.World);
        }
    }

    private void Jump()
    {
        if (is_on_ground) //判断是否在地面上
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (counter_time < max_time)
                {  //如果当前蓄力值小于最大值
                    counter_time += 0.02f; //则每帧递增当前蓄力值
                }
                else
                {  //达到最大值时，当前蓄力值就等于最大蓄力值

                    jumo_audio.Play();
                    jumpPressure = MaxjumpPressure;
                    rbody.velocity = new Vector3(0f, jumpPressure, 0f);
                    jumpPressure = 0f; //升空以后把蓄力值重设为0
                    is_on_ground = false;  //在地面上设为否
                    counter_time = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {   //如果是轻轻按下就松开则把最小蓄力值赋值给当前蓄力值
                //如果是按住不松则把上面递增的值传下来
                //给一个向上速度
                jumo_audio.Play();
                jumpPressure = 17000f / 9f * counter_time * counter_time - 130f / 9f * counter_time + 16f;
                rbody.velocity = new Vector3(0f, jumpPressure, 0f);
                jumpPressure = 0f; //升空以后把蓄力值重设为0
                is_on_ground = false;  //在地面上设为否
                counter_time = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                rbody.gravityScale = 22;
            if (double_jump)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumo_audio.Play();
                    rbody.gravityScale = 9.8f;
                    jumpPressure = MaxjumpPressure;
                    rbody.velocity = new Vector3(0f, jumpPressure, 0f);
                    jumpPressure = 0f; //升空以后把蓄力值重设为0
                    is_on_ground = false;  //在地面上设为否
                    double_jump = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "ground":
                is_on_ground = true;
                double_jump = true;
                rbody.gravityScale = 9.8f;
                break;
            case "barrier":
                Destroy(gameObject);
                Scene_manager.Instance.is_dead = true;
                Debug.LogFormat("die_collision");
                break;
        }
    }
}
