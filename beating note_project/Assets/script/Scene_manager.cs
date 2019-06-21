using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_manager : MonoBehaviour
{
    public int Scene_num;
    public bool is_dead = false;
    public int life_value;

    private void Awake()
    {
        Instance = this;
        ////// 防止载入新场景时被销毁
        //if(!can_be_destroyed)
        //DontDestroyOnLoad(instance.gameObject);
    }

    private static Scene_manager instance;

    public static Scene_manager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
        Debug.LogFormat("loadScene:{0}, life_value:{1}",num,life_value);
        Scene_num = num;
    }
}
