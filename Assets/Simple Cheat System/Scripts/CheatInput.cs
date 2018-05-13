using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType { InputBox, KeyPress }
namespace SimpleCheatSystem
{
    /// <summary>
    /// CLASS: Handles code input from inputbox and keypress
    /// </summary>
    public class CheatInput : MonoBehaviour
    {
        [Tooltip("Choose between Inputbox or keypress")]
        public InputType inputType;
        [Tooltip("Time allocated to type cheat in")]
        public float cheatTime = 1f;
        [Space(10)]
        public GameObject cheatPanel;
        public InputField inputField;
        public Text cheatText;
        private List<string> cheatCache = new List<string>(); //make public to see combo in inspector


        private bool boolToggle = false;
        float timeIterator = 0;
        bool firstInput = false;
        private int stepInput1 = 0;
        private int stepInput2 = 0;
        private string cheatString = "";
        public static CheatInput cInstance;
        private void Start()
        {
            cInstance = this;
        }


        private void Update()
        {

            InputCheck();
            Timekeeper();

        }
        bool istoggle;
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
                    if (istoggle == false)
                    {
                        TxtEnum("Cheat Enabled");
                        istoggle = !istoggle;
                    }
                    else if (istoggle == true)
                    { TxtEnum("Cheat Disabled"); istoggle = !istoggle; }
                    CheatEngine.cheatEnabled = !CheatEngine.cheatEnabled;

                }
                if (CheatEngine.cheatEnabled == true)
                {
                    // print("Cheat enabled");
                    foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(kcode))
                        {
                            // Debug.Log("KeyCode down: " + kcode);

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
        public void TxtEnum(string txt)
        {
            StartCoroutine(StartEnum(txt));
        }

        public IEnumerator StartEnum(string textValue)
        {
            cheatText.gameObject.SetActive(true);
            cheatText.text = textValue;
            yield return new WaitForSeconds(.8f);
            if (cheatText.color != Color.clear)
            {
                Color newCol;
                if (ColorUtility.TryParseHtmlString("#FFFFFF00", out newCol))
                    cheatText.color = Color.Lerp(cheatText.color, newCol, 1.2f * Time.deltaTime);
            }
            yield return new WaitForSeconds(1f);
            cheatText.color = Color.white;
            cheatText.gameObject.SetActive(false);

        }
    }
}
