using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    private GameObject p1LHand, p1RHand, p2LHand, p2RHand;
    private Player p1Scr, p2Scr;
    private bool p1triggered, p2triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        p1LHand = p1.transform.GetChild(0).gameObject;
        p1RHand = p1.transform.GetChild(1).gameObject;

        p2LHand = p2.transform.GetChild(0).gameObject;
        p2RHand = p2.transform.GetChild(1).gameObject;

        p1Scr = p1.GetComponent<Player>();
        p2Scr = p2.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && p1triggered)
        { 
            if (p1Scr.handLFull)
            {
                Destroy(p1Scr.holding[0]);
                p1Scr.handLFull = false;
            }

            else if (p1Scr.handRFull)
            {
                Destroy(p1Scr.holding[1]);
                p1Scr.handRFull = false;
            }
        }

        if (Input.GetKeyDown(";") && p2triggered)
        {
            if (p2Scr.handLFull)
            {
                Destroy(p2Scr.holding[0]);
                p2Scr.handLFull = false;
            }

            else if (p2Scr.handRFull)
            {
                Destroy(p2Scr.holding[1]);
                p2Scr.handRFull = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            p1triggered = true;
        }

        if (collision.gameObject.name == "Player 2")
        {
            p2triggered = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1")
        {
            p1triggered = false;
        }

        if (collision.gameObject.name == "Player 2")
        {
            p2triggered = false;
        }
    }
}
