using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
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
        if (p1triggered && Input.GetKeyDown("f"))
        {
            if (p1Scr.handLFull == false)
            {
                // duplicates vegetable, and sets the position to the correct hand
                GameObject currentVeg = GameObject.Instantiate(this.gameObject);
                currentVeg.transform.position = new Vector2(p1LHand.transform.position.x, p1LHand.transform.position.y);
                currentVeg.transform.SetParent(p1.transform);

                // adds vegetable to holding list 
                p1Scr.holding[0] = currentVeg;
                p1Scr.handLFull = true;
            }
            else if (p1Scr.handRFull == false)
            {
                GameObject currentVeg = GameObject.Instantiate(this.gameObject); // duplicates the selected vegetable
                currentVeg.transform.position = new Vector2(p1RHand.transform.position.x, p1RHand.transform.position.y);
                currentVeg.transform.SetParent(p1.transform);

                p1Scr.holding[1] = currentVeg;
                p1Scr.handRFull = true;
            }
        }

        if (p2triggered && Input.GetKeyDown(";"))
        {
            if (p2Scr.handLFull == false)
            {
                GameObject currentVeg = GameObject.Instantiate(this.gameObject); // duplicates the selected vegetable
                currentVeg.transform.position = new Vector2(p2LHand.transform.position.x, p2LHand.transform.position.y);
                currentVeg.transform.SetParent(p2.transform);

                p2Scr.holding[0] = currentVeg;
                p2Scr.handLFull = true;
            }
            else if (p2Scr.handRFull == false)
            {
                GameObject currentVeg = GameObject.Instantiate(this.gameObject); // duplicates the selected vegetable
                currentVeg.transform.position = new Vector2(p2RHand.transform.position.x, p2RHand.transform.position.y);
                currentVeg.transform.SetParent(p2.transform);

                p2Scr.holding[1] = currentVeg;
                p2Scr.handRFull = true;
            }
        }
    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        // Player 1
        if (collision.gameObject.name == "Player 1" )
        {
            p1triggered = true;           
        }

        // Player 2
        if (collision.gameObject.name == "Player 2")
        {
            p2triggered = true;
        } 
    }
    void OnTriggerExit2D(Collider2D collision)
    {

        // Player 1
        if (collision.gameObject.name == "Player 1")
        {
            p1triggered = false;
        }

        // Player 2
        if (collision.gameObject.name == "Player 2")
        {
            p2triggered = false;
        }
    }
}
