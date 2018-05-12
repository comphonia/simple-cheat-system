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
        static List<string> inputCache = new List<string>();
        static float time;
        public static CheatEngine instance;
        public static CheatInput cInput;


        public void Awake()
        {
            instance = this;
        }


        public static void CheckEntry(string input)
        {
            cInput = CheatInput.cInstance;
            string userInput = input.Trim().ToLower();
            for (int i = 0; i < CheatList.Count; i++)
            {
                //Activate Cheat
                if (userInput == CheatList[i].Combo && !inputCache.Contains(userInput))
                {
                    inputCache.Add(userInput);
                    Debug.Log(CheatList[i].name);
                    CheatList[i].onCheatActivated.Invoke();
                    cInput.StartEnum(CheatList[i].name + " Activated");
                    if (!CheatList[i].isPersistent)
                    {
                        instance.StartCoroutine(Timer(CheatList[i].disableTime, i));
                        inputCache.Remove(userInput);

                    }
                    break;
                }
                // Deactivate Cheat
                else if (userInput == CheatList[i].Combo && inputCache.Contains(userInput))
                {
                    if (CheatList[i].isPersistent)
                    {
                        CheatList[i].disableCheat.Invoke();
                        inputCache.Remove(userInput);
                        cInput.StartEnum(CheatList[i].name + " Deactivated");
                    }
                    break;
                }
            }
        }

        public static void SetCheatLibrary(List<Cheat> list)
        {
            CheatList = new List<Cheat>(list);
        }

        private static IEnumerator Timer(float time, int index)
        {
            yield return new WaitForSeconds(time);
            CheatList[index].disableCheat.Invoke();
        }
    }
}
