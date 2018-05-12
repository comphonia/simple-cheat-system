using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{



    public int player_health = 100;
    public float moveSpeed = 1;
    public bool isDead = false;
    public bool godMode = false;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    // private bool isOn = true;
    void Update()
    {
        if (godMode == true)
        {
            player_health = 100;
        }


        if (player_health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
        //Movement
        if (isDead == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
            transform.Translate(movement * Time.deltaTime * moveSpeed);
        }



    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Enemy")
        {
            player_health -= 10;

        }
    }

}