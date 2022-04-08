using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = .05f;
    public bool player1 = true;
    public bool handLFull = false;
    public bool handRFull = false;
    public GameObject[] holding = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player Controls
        if (player1)
        {
            if (Input.GetKey("w"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed);
            }

            if (Input.GetKey("s"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed);
            }

            if (Input.GetKey("d"))
            {
                transform.position = new Vector2(transform.position.x + speed, transform.position.y);
            }

            if (Input.GetKey("a"))
            {
                transform.position = new Vector2(transform.position.x - speed, transform.position.y);
            }
        }
        else
        {
            if (Input.GetKey("i"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed);
            }

            if (Input.GetKey("k"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed);
            }

            if (Input.GetKey("l"))
            {
                transform.position = new Vector2(transform.position.x + speed, transform.position.y);
            }

            if (Input.GetKey("j"))
            {
                transform.position = new Vector2(transform.position.x - speed, transform.position.y);
            }
        }
    }
}
