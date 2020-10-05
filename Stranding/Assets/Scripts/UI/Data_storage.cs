using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stranding
{
    ///<summary>
    /// used to store user data
    /// </summary>
    
    public class Data_storage : MonoBehaviour
    {
        
        private int _health;
        public int Health
        {
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _health = value;
                }
            }

            get
            {
                return _health;
            }
        }

        private int _loop;
        public int Loop
        {
            set
            {
                if (value >= 0)
                {
                    _loop = value;
                }
            }

            get
            {
                return _loop;
            }
        }

        private int _step;
        public int Step
        {
            set
            {
                if (value >= 0)
                {
                    _step = value;
                }
            }

            get
            {
                return _step;
            }
        }

        // control set _trigger_dice_roll to true
        // -> dice set _trigger_dice_roll to false and roll
        // -> after roll compelte dice set _last_dice_outcome
        // -> after _last_dice_outcome changed
        // -> control read _last_dice_outcome when Is_dice_updated is true
        // -> after read control set Is_dice_updated to false

        private bool _trigger_dice_roll;
        public bool Trigger_dice_roll
        {
            set { _trigger_dice_roll = value; }
            get { return _trigger_dice_roll; }
        }

        private bool _is_dice_updated;
        public bool Is_dice_updated
        {
            set { _is_dice_updated = value; }
            get { return _is_dice_updated; }
        }

        private int _last_dice_outcome;
        public int Last_dice_outcome
        {
            set { _last_dice_outcome = value; }
            get { return _last_dice_outcome; }
        }

        private bool _is_at_station;
        public bool Is_at_station
        {
            set { _is_at_station = value; }
            get { return _is_at_station; }
        }

        public void Start()
        {
            Health = 100;
            Loop = 0;
            Step = 0;
            Last_dice_outcome = 0;
            Trigger_dice_roll = false;
            Is_dice_updated = false;
            Is_at_station = false;
        }
    }
}

