using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCheats : MonoBehaviour
{


    bool isToggle;


    public void InverseGravity()
    {
        Physics.gravity = -Physics.gravity;

    }
}
