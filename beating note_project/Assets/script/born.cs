using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class born : MonoBehaviour
{
    public Vector3 born_x_y;//复活点
    public GameObject playerPrefab;
    public Text life_value_text;


    private void Start()
    {
        Instantiate(playerPrefab,born_x_y, Quaternion.identity);
    }

    private void Update()
    {
        if (Scene_manager.Instance.is_dead)
        {
            bornplayer();
            Debug.LogFormat("loadScene:{0}, life_value:{1}", Scene_manager.Instance.Scene_num, Scene_manager.Instance.life_value);
        }
    }

    private void bornplayer()
    {
        Instantiate(playerPrefab, born_x_y, Quaternion.identity);
        Scene_manager.Instance.is_dead = false;
        if (Scene_manager.Instance.Scene_num != 0)
        {
            Scene_manager.Instance.life_value+=1;
            life_value_text.text = Scene_manager.Instance.life_value.ToString();
        }
    }
}