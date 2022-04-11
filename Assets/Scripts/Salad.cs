using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : MonoBehaviour
{
    // variables
    List<string> ingredients = new List<string>();
    bool inLeftHand, inRightHand = false;

    // properties
    public List<string> Ingredients
    {
        get { return ingredients; }
        set { ingredients = value; }
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
}
