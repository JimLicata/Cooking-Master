using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variables
    [SerializeField] bool player1 = true;
    float speed = .05f;  
    bool handLFull = false;
    bool handRFull = false;
    GameObject leftHand, rightHand;
    List<GameObject> holding = new List<GameObject>();
    bool chopping = false;

    // properties
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public bool HandLFull
    {
        get { return handLFull; }
        set { handLFull = value; }
    }
    public bool HandRFull
    {
        get { return handRFull; }
        set { handRFull = value; }
    }
    public GameObject LeftHand
    {
        get { return leftHand; }
        set { leftHand = value; }
    }
    public GameObject RightHand
    {
        get { return rightHand; }
        set { rightHand = value; }
    }
    public bool Chopping
    {
        get { return chopping; }
        set { chopping = value; }
    }
    public List<GameObject> Holding
    {
        get { return holding; }
        set { holding = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        leftHand = transform.GetChild(0).gameObject;
        rightHand = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Controls
        if (!chopping) // checks to see if the player can move
        {
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
}
