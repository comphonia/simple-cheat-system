using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType { InputBox, KeyPress }
namespace SimpleCheatSystem
{
    public class CheatInput : MonoBehaviour
    {
        public InputType inputType;
        [Header("UI Settings")]
        public GameObject cheatPanel;
        public InputField inputField;
        public List<string> cheatCache = new List<string>();
        public float cheatTime = 1f;

        private bool boolToggle = false;
        private int stepInput1 = 0;
        private int stepInput2 = 0;
        float timeIterator = 0;
        bool firstInput = false;
        private string cheatString = "";

        private void Start()
        {

        }


        private void Update()
        {

            InputCheck();
            Timekeeper();
        }
        // Chooses UI input box or regular input
        private void InputCheck()
        {
            if (inputType == InputType.InputBox)
            {

                if (Input.GetButtonDown("Cancel")) //Toggle is activated with the Cancel button (Escape Key)
                {
                    CheatEngine.cheatEnabled = true;
                    boolToggle = !boolToggle;
                    // If bool is true show input field
                    if (boolToggle == true)
                    {
                        ShowPanel();
                    }

                    if (boolToggle == false)
                    {
                        HidePanel();
                    }
                }

                if (inputField.isFocused && inputField.text != "" && Input.GetKey(KeyCode.Return))
                {
                    // inputField.text = "";
                    HidePanel();
                }
            }

            else if (inputType == InputType.KeyPress)
            {
                if (stepInput1 == 0)
                {
                    CheatEngine.cheatEnabled = false;
                    stepInput1++;
                }
                HidePanel();
                if (Input.GetButtonDown("Cancel"))
                {
                    CheatEngine.cheatEnabled = !CheatEngine.cheatEnabled;

                }
                if (CheatEngine.cheatEnabled == true)
                {
                    // print("Cheat enabled");
                    foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(kcode))
                        {
                            Debug.Log("KeyCode down: " + kcode);
                            if (kcode == KeyCode.Escape)
                            {

                            }
                            if (kcode != KeyCode.Escape && timeIterator < cheatTime)
                            {
                                if (stepInput2 == 0)
                                {
                                    firstInput = true;
                                    stepInput2++;
                                }
                                cheatCache.Add(kcode.ToString());
                                cheatString += kcode.ToString();
                                //   firstInput = true;
                            }

                        }

                    }
                }
                //play sound
                //show cheat activated text top right
            }
        }


        private void ShowPanel()
        {
            Time.timeScale = 0;
            cheatPanel.SetActive(true);
            inputField.Select();
        }

        private void HidePanel()
        {
            Time.timeScale = 1;
            cheatPanel.SetActive(false);
            boolToggle = false;
            //? CheatEngine.cheatEnabled = false;
        }

        // This Method passes string input to the CheatEngine class to verify if valid
        public void PassEntry(string _cheatString = "")
        {
            HidePanel();
            if (inputType == InputType.InputBox)
            {
                if (inputField.text != "")
                {
                    //? print(inputField.text);
                    CheatEngine.CheckEntry(inputField.text.Trim().ToLower());
                    inputField.text = "";
                }
            }
            else if (inputType == InputType.KeyPress)
            {
                CheatEngine.CheckEntry(_cheatString.Trim().ToLower());
            }

        }

        // Method clears out the cheatCache list
        private void ClearCache()
        {
            do
            {
                for (int i = 0; i < cheatCache.Count; i++)
                {
                    cheatCache.RemoveAt(i);
                }
            } while (cheatCache.Count > 0);


        }

        // Method checks if input has been made within appropriate time
        private void Timekeeper()
        {
            if (firstInput == true)
            {
                timeIterator += Time.deltaTime; // increases the float timeIterator

                //? print(timeIterator);
                if (timeIterator >= cheatTime)
                {
                    timeIterator = 0;
                    firstInput = false;
                    ClearCache();
                    PassEntry(cheatString);
                    cheatString = "";
                    stepInput2 = 0;
                }
            }
        }
    }
}
