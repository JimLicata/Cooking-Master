using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customer : MonoBehaviour
{
    // variables
    [SerializeField] Player p1, p2;
    [SerializeField] TextMeshPro textComponent;
    bool p1triggered, p2triggered = false;
    List<string> order = new List<string>();
    string[] vegetables = new string[] { "A", "B", "C", "D", "E", "F" };
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && p1triggered)
        {
            Place(p1);
        }

        if (Input.GetKeyDown("o") && p2triggered)
        {
            Place(p2);
        }
    }

    void GenerateOrder()
    {
        int num = Random.Range(2, 4); // determins how many ingredients
        string veg = "";
        for (int i = 0; i < num; i++)
        {
            // makes sure there are no duplicate ingredients
            bool passed = false;
            while (!passed)
            {
                veg = vegetables[Random.Range(0, 6)];
                if (order.Contains(veg))
                {
                    passed = false;
                }
                else
                {
                    passed = true;
                }
            }
            
            order.Add(veg); // adds ingredients to the order
        }

        foreach (string ingredient in order)
        {
            textComponent.text += ingredient; // updates the text to reflect the order
        }
    }

    void Place(Player player)
    {
        if (player.Holding.Count > 0 && player.Holding[0].GetComponent<Vegetable>().IsChopped) // checks if the player is holding anything and if it is salad
        {
            // tests to make sure the order and salad ingredients match
            bool passed = true;
            foreach (string ingredient in player.Holding[0].GetComponent<Vegetable>().Ingredients)
            {
                if (order.Contains(ingredient) == false) // if the ingredient is not in the order
                {
                    passed = false;
                }
            }
            foreach (string ingredient in order)
            {
                if (player.Holding[0].GetComponent<Vegetable>().Ingredients.Contains(ingredient) == false) // if the ingredient is not in the order
                {
                    passed = false;
                }
            }

            if (passed) // the salad is correct
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
            else
            {
                Debug.Log("Incorrect");
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
