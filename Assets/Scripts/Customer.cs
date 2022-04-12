using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    // variables
    [SerializeField] Player p1, p2;
    [SerializeField] TextMeshPro orderText, timerText;
    [SerializeField] GameManager gm;
    [SerializeField] GameObject powerUp;
    bool p1triggered, p2triggered = false;
    List<string> order = new List<string>();
    string[] vegetables = new string[] { "A", "B", "C", "D", "E", "F" };
    float timer = 60;
    float startTime = 60;
    float anger = 1;

    public void Initialize()
    {
        timer = 60;
        startTime = 60;
        anger = 1;
        order.Clear();
        orderText.text = "";
        GenerateOrder();
    }

    // Start is called before the first frame update
    void Start()
    {       
        //GenerateOrder();
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

        if (timer > 0)
        {
            timer -= Time.deltaTime * anger;
            timerText.text = string.Format("{0:00}", timer);
        }
        else
        {
            gameObject.SetActive(false);
            gm.CustomerCount--;
            p1.Score -= 10;
            p2.Score -= 10;
        }
    }

    void GenerateOrder()
    {

        int num = Random.Range(2, 4); // determins how many ingredients
        if (num == 2)
        {
            startTime = 45;
        }
        else
        {
            startTime = 60;
        }
        timer = startTime;
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
            orderText.text += ingredient; // updates the text to reflect the order
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
                gameObject.SetActive(false);
                player.Score += 10;

                if (timer >  startTime - (startTime * .7) ) // before 70% of wait time
                {
                    PowerUp(player);
                }
            }
            else
            {
                anger = 2; // customer is angry
                player.Score -= 30;
            }
        }
    }

    void PowerUp(Player player)
    {
        powerUp.GetComponent<PowerUp>().IsSpeedUp = false;
        powerUp.GetComponent<PowerUp>().IsTimeIncrease = false;
        powerUp.GetComponent<PowerUp>().IsBonusPoints = false;

        // randomly determine which power up 
        float num = Random.Range(0, 3);
        switch(num)
        {
            case 0:               
                powerUp.GetComponent<PowerUp>().IsSpeedUp = true;
                break;
            case 1:
                powerUp.GetComponent<PowerUp>().IsTimeIncrease = true;
                break;
            case 2:
                powerUp.GetComponent<PowerUp>().IsBonusPoints = true;
                break;
        }

        // set which player can collect 
        if (player.Player1)
        {
            powerUp.GetComponent<PowerUp>().ForPlayer1 = true;
        }
        else
        {
            powerUp.GetComponent<PowerUp>().ForPlayer1 = false;
        }
        float x = Random.Range(-6f, 7f);
        float y = Random.Range(-3f, 3f);
        Instantiate(powerUp, new Vector2(x, y), Quaternion.identity);
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
