using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next_level : MonoBehaviour
{

    public int next_level_scene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("note"):
                Scene_manager.Instance.LoadScene(next_level_scene);
                break;
        }
    }
}
