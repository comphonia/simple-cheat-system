using System;
using UnityEngine;
using UnityEngine.Events;

namespace SimpleCheatSystem
{
    [Serializable]
    public class Cheat
    {
        /// <summary>
        /// CLASS: creates individual cheat objects
        /// </summary>
        /// 
        [Tooltip("The name of the cheat code")]
        public string name;
        [Tooltip("Cheat string value to be compared")]
        public string combo;
        [Tooltip("If persistent is false, disableCheat is activated after disableTime")]
        public bool isPersistent = true;
        [Tooltip("Time before cheat auto-disables, doesn't auto-disable if not persistent")]
        public float disableTime = 3f;
        public UnityEvent onCheatActivated;
        public UnityEvent disableCheat;

        public Cheat(string _name, string _combo, bool _isPersistent)
        {
            Name = _name;
            Combo = _combo;
            isPersistent = _isPersistent;
        }
        public Cheat()
        {

        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Combo
        {
            get
            {
                return combo;
            }
            //
            set
            {
                combo = value;
            }
        }
    }
}
