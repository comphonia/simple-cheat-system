using System;
using UnityEngine.Events;

namespace SimpleCheatSystem
{
    [Serializable]
    public class Cheat
    {
        /// <summary>
        /// CLASS: creates individual cheat objects
        /// </summary>
        public string name;
        public string combo;
        public bool isPersistent = true;
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
