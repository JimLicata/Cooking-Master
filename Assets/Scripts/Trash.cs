using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    //variables
    [SerializeField] Player p1, p2;
    bool p1triggered, p2triggered = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && p1triggered)
        {
            ThrowOut(p1);
        }

        if (Input.GetKeyDown("o") && p2triggered)
        {
            ThrowOut(p2);
        }
    }

    void ThrowOut(Player player)
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
            Destroy(player.Holding[0]);
            player.Holding.RemoveAt(0);
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
