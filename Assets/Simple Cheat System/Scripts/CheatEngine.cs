using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SimpleCheatSystem
{
    /// <summary>
    /// CLASS: This class stores the static list of cheats and compares input with list codes
    /// </summary>
    class CheatEngine : MonoBehaviour
    {
        public static bool cheatEnabled = false;
        static List<Cheat> CheatList;
        static float time;
        private static CheatEngine instance;

        public void Awake()
        {
            instance = this;

        }


        public static void CheckEntry(string input, string compareTo = null)
        {

            for (int i = 0; i < CheatList.Count; i++)
            {
                if (input.Trim().ToLower() == CheatList[i].Combo)
                {
                    Debug.Log(CheatList[i].name);
                    CheatList[i].onCheatActivated.Invoke();
                    if (!CheatList[i].isPersistent)
                    {
                        instance.StartCoroutine(Timer(CheatList[i].disableTime, 0));

                    }
                    break;
                }
                else
                {
                    Debug.Log("Wrong Input.");
                }
            }
        }

        public static void SetCheatLibrary(List<Cheat> list)
        {
            CheatList = new List<Cheat>(list);
        }

        private static IEnumerator Timer(float time, int index)
        {
            //  Debug.Log(time);
            yield return new WaitForSeconds(time);
            CheatList[index].disableCheat.Invoke();
        }
    }
}
