using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_console_control : MonoBehaviour
{
    /// <summary>
    /// example data storage use case
    /// </summary>
    
    /// 1)
    // player data storage game object
    public GameObject source;
    // local data storage script object
    private Data_storage data_storage;



    private int health;
    private int loop;
    private int step;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        loop = 0;
        step = 0;


        // 2)
        // initialize data storage
        data_storage = source.GetComponent<Data_storage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (health < 100)
            {
                health += 1;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (health > 0)
            {
                health -= 1;
            }
        }


        // 3)
        // put data to data storage
        data_storage.Set_health(health);
    }
}
