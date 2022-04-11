using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    // variables
    [SerializeField] Player p1, p2;   
    bool p1triggered, p2triggered = false;
    bool inLeftHand, inRightHand = false;
    bool isChopped = false;
    bool isSalad = false;
    List<string> ingredients = new List<string>();

    // properties
    public bool P1triggered
    {
        get { return p1triggered; }
        set { p1triggered = value; }
    }
    public bool P2triggered
    {
        get { return p2triggered; }
        set { p2triggered = value; }
    }
    public bool InLeftHand
    {
        get { return inLeftHand; }
        set { inLeftHand = value; }
    }
    public bool InRightHand
    {
        get { return inRightHand; }
        set { inRightHand = value; }
    }
    public bool IsChopped
    {
        get { return isChopped; }
        set { isChopped = value; }
    }
    public bool IsSalad
    {
        get { return isSalad; }
        set { isSalad = value; }
    }
    public List<string> Ingredients
    {
        get { return ingredients; }
        set { ingredients = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (p1triggered && Input.GetKeyDown("e"))
        {
            PickUp(p1);
        }

        if (p2triggered && Input.GetKeyDown("o"))
        {
            PickUp(p2);
        }  
    }

    void PickUp(Player player)
    {
        if (player.HandLFull == false)
        {
            // duplicates vegetable, and sets the position to the correct hand
            GameObject currentVeg = GameObject.Instantiate(this.gameObject);
            currentVeg.transform.position = new Vector2(player.LeftHand.transform.position.x, player.LeftHand.transform.position.y);
            currentVeg.transform.SetParent(player.transform);

            // adds vegetable to holding list 
            player.Holding.Add(currentVeg);
            player.HandLFull = true;
            currentVeg.GetComponent<Vegetable>().inLeftHand = true;
        }
        else if (player.HandRFull == false)
        {
            GameObject currentVeg = GameObject.Instantiate(this.gameObject); // duplicates the selected vegetable
            currentVeg.transform.position = new Vector2(player.RightHand.transform.position.x, player.RightHand.transform.position.y);
            currentVeg.transform.SetParent(player.transform);

            player.Holding.Add(currentVeg);
            player.HandRFull = true;
            currentVeg.GetComponent<Vegetable>().inRightHand = true;
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
