using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // variables
    [SerializeField] Player p1, p2;
    [SerializeField] Customer[] customers = new Customer[5];
    int customerCount = 0;

    // properties
    public int CustomerCount
    {
        get { return customerCount; }
        set { customerCount = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        customers[Random.Range(0, 5)].gameObject.SetActive(true);
        customerCount++;
        StartCoroutine(NewCustomer());
    }

    // Update is called once per frame
    void Update()
    {
        // adds new customer if none are active
        if (customerCount == 0)
        {
            AddCustomer(Random.Range(0, 5));
        }
    }

    IEnumerator NewCustomer()
    {
        int time = Random.Range(10, 20); // random time until next customer arrives
        yield return new WaitForSeconds(time);

        // enables another random customer 
        bool passed = false;
        while (!passed) 
        {
            int index = Random.Range(0, 5);
            // makes sure the same customer isn't activated twice
            if (customers[index].gameObject.activeSelf == false) 
            {
                AddCustomer(index);
                passed = true;
            }
            if (customerCount >= 5) // if all spots are full, breaks loop
            {
                passed = true;
                StartCoroutine(NewCustomer());
            }
        }
    }

    void AddCustomer(int index)
    {
        customers[index].gameObject.SetActive(true);
        customerCount++;
        StartCoroutine(NewCustomer());
    }
}
