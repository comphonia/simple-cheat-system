using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string name;
        public string combo;
        public bool isPersistent = true;
        public UnityEvent onCheatActivated;
        public UnityEvent ifNotPersistent;

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
