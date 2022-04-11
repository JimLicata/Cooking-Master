using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoard : MonoBehaviour
{
    [SerializeField] Player p1, p2;
    [SerializeField] GameObject salad;
    Vegetable saladScr;
    bool p1triggered, p2triggered = false;
    bool saladComplete = false;
    List<GameObject> vegetables = new List<GameObject>();

    void Start()
    {
        saladScr = salad.GetComponent<Vegetable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && p1triggered)
        {
            if (vegetables.Count < 3)
            {
                StartCoroutine(Chop(p1));
            }
        }

        if (Input.GetKeyDown("f") && p1triggered)
        {
            PickUp(p1);
        }

        if (Input.GetKeyDown("o") && p2triggered)
        {
            if (vegetables.Count < 3)
            {
                StartCoroutine(Chop(p2));
            }
        }

        if (Input.GetKeyDown(";") && p2triggered)
        {
            PickUp(p2);
        }
    }

    void PickUp(Player player)
    {
        if (saladComplete)
        {
            if (player.HandLFull == false)
            {
                foreach (GameObject vegetable in vegetables)
                {
                    // trims name to be a single identifying letter
                    string[] words = vegetable.name.Split(' ');
                    string[] name = words[1].Split('(');                   
                    saladScr.Ingredients.Add(name[0]);
                }

                GameObject currentSalad = GameObject.Instantiate(salad);

                // moves salad position to the correct hand
                currentSalad.transform.position = new Vector2(player.LeftHand.transform.position.x, player.LeftHand.transform.position.y);
                currentSalad.transform.SetParent(player.transform);

                // copies salad to holding list 
                currentSalad.GetComponent<Vegetable>().InLeftHand = true;
                currentSalad.GetComponent<Vegetable>().IsChopped = true;
                player.Holding.Add(currentSalad);
                player.HandLFull = true;
                saladScr.Ingredients.Clear();

                // removes ingredients from chopping board
                foreach (GameObject vegetable in vegetables)
                {
                    Destroy(vegetable);
                }
                vegetables.Clear();
                saladComplete = false;
            }
            else if (player.HandRFull == false)
            {
                foreach (GameObject vegetable in vegetables)
                {
                    saladScr.Ingredients.Add(vegetable.gameObject.name);
                }

                GameObject currentSalad = GameObject.Instantiate(salad);

                // moves salad position to the correct hand
                currentSalad.transform.position = new Vector2(player.RightHand.transform.position.x, player.RightHand.transform.position.y);
                currentSalad.transform.SetParent(player.transform);

                // copies salad to holding list 
                currentSalad.GetComponent<Vegetable>().InRightHand = true;
                player.Holding.Add(currentSalad);
                player.HandRFull = true;
                saladScr.Ingredients.Clear();

                // removes ingredients from chopping board
                foreach (GameObject vegetable in vegetables)
                {
                    Destroy(vegetable);
                }
                vegetables.Clear();
                saladComplete = false;
            }
        }
    }

    IEnumerator Chop(Player player)
    {
        if (!player.Chopping)
        {
            if (player.Holding.Count > 0 && player.Holding[0].GetComponent<Vegetable>().IsChopped == false) // checks if the player is holding anything
            {
                player.Chopping = true;

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
                vegetables.Add(player.Holding[0]);

                switch (vegetables.Count)
                {
                    case 1:
                        vegetables[0].transform.position = new Vector2(transform.position.x - .5f, transform.position.y);
                        vegetables[0].transform.parent = null;
                        vegetables[0].GetComponent<Vegetable>().IsChopped = true;
                        break;
                    case 2:
                        vegetables[1].transform.position = transform.position;
                        vegetables[1].transform.parent = null;
                        vegetables[1].GetComponent<Vegetable>().IsChopped = true;
                        break;
                    case 3:
                        vegetables[2].transform.position = new Vector2(transform.position.x + .5f, transform.position.y);
                        vegetables[2].transform.parent = null;
                        vegetables[2].GetComponent<Vegetable>().IsChopped = true;
                        break;
                }
               
                player.Holding.RemoveAt(0);

                yield return new WaitForSeconds(2);
                // TODO change sprite to chopped sprite here 
                player.Chopping = false;
                saladComplete = true;
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

