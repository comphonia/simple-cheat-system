using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public List<int> list;
    // Use this for initialization
    void Start()
    {
        do
        {
            for (int i = 0; i < list.Count; i++)
            {
                list.RemoveAt(i);
            }
        } while (list.Count > 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
