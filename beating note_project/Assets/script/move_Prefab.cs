using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_Prefab : MonoBehaviour
{

    public GameObject Prefab;//预制体
    public float speed;
    public float time;
    public int is_right_first;//1为右，-1为左
    private float time_count = 0;
    public bool is_up;//最后一关上移的刺
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            time_count += Time.deltaTime;
            //Debug.LogFormat("{0}", time_count);
            if(time_count > time)
            {
                is_right_first = -is_right_first;
                time_count = 0;
            }
        if (is_up)
        {
            Prefab.transform.Translate(new Vector3(0, speed * Time.deltaTime), Space.World);

        }
        else
            Prefab.transform.Translate(new Vector3(speed * Time.deltaTime * is_right_first, 0), Space.Self);
    }
}
