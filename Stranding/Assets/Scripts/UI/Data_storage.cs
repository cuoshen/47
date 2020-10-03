using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_storage : MonoBehaviour
{
    ///<summary>
    /// used to store user data
    /// </summary>

    private int health;
    private int loop;
    private int step;

    public Data_storage()
    {
        health = 100;
        loop = 0;
        step = 0;
    }

    // getters
    public int Get_health()
    {
        return this.health;
    }

    public int Get_loop()
    {
        return this.loop;
    }

    public int Get_step()
    {
        return this.step;
    }

    // settters
    public void Set_health(int _health)
    {
        if (_health >= 0 && _health <= 100)
        {
            this.health = _health;
        }
    }

    public void Set_loop(int _loop)
    {
        if (_loop >= 0)
        {
            this.loop = _loop;
        }
    }

    public void Set_step(int _step)
    {
        if (_step >= 0)
        {
            this.step = _step;
        }
    }
}
