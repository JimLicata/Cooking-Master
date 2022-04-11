using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    // variables
    [SerializeField] Player p1, p2;
    bool p1triggered, p2triggered = false;
    GameObject vegetable;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && p1triggered)
        {
            if (vegetable == null)
            {
                Place(p1);
            }
            else
            {
                PickUp(p1);
            }
        }

        if (Input.GetKeyDown("o") && p2triggered)
        {
            if (vegetable == null)
            {
                Place(p2);
            }
            else
            {
                PickUp(p2);
            }
        }
    }

    void Place(Player player)
    {
        if (player.Holding.Count > 0) // checks if the player is holding anything
        {
            // determines which hand the first vegetable is in and empties it
            if (player.Holding[0].GetComponent<Vegetable>().InLeftHand)
            {
                player.HandLFull = false;
            }
            else if (player.Holding[0].GetComponent<Vegetable>().InRightHand)
            {
                player.HandRFull = false;
            }

            // determines what the first vegetable is and moves it to plate
            vegetable = player.Holding[0];
            vegetable.transform.position = this.transform.position;
            vegetable.transform.parent = null;
            player.Holding.RemoveAt(0);
        }       
    }

    void PickUp(Player player)
    {
        if (player.HandLFull == false)
        {
            // moves vegetable position to the correct hand
            vegetable.transform.position = new Vector2(player.LeftHand.transform.position.x, player.LeftHand.transform.position.y);
            vegetable.transform.SetParent(player.transform);

            // copies vegetable to holding list 
            vegetable.GetComponent<Vegetable>().InLeftHand = true;
            player.Holding.Add(vegetable);
            player.HandLFull = true;
            vegetable = null;
        }
        else if (player.HandRFull == false)
        {
            // moves vegetable position to the correct hand
            vegetable.transform.position = new Vector2(player.RightHand.transform.position.x, player.RightHand.transform.position.y);
            vegetable.transform.SetParent(player.transform);

            // copies vegetable to holding list 
            vegetable.GetComponent<Vegetable>().InRightHand = true;
            player.Holding.Add(vegetable);
            player.HandRFull = true;
            vegetable = null;
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
