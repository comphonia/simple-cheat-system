using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCheatSystem
{
    /// <summary>
    /// CLASS: Factory stores cheat code name and cheats in a list
    /// </summary>
    public class CheatFactory : MonoBehaviour
    {
        public List<Cheat> cheatCodes = new List<Cheat>();

        // Use this for initialization
        void Start()
        {
            CheatEngine.SetCheatLibrary(cheatCodes);
            gameObject.AddComponent<CheatEngine>();
        }

    }
}
