using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUp : MonoBehaviour
{
    [SerializeField] Player p1, p2;
    [SerializeField] TextMeshPro label;
    public bool forPlayer1;
    public bool isSpeedUp = false;
    public bool isTimeIncrease = false;
    public bool isBonusPoints = false;

    public bool ForPlayer1
    {
        get { return forPlayer1; }
        set { forPlayer1 = value; }
    }
    public bool IsSpeedUp
    {
        get { return isSpeedUp; }
        set { isSpeedUp = value; }
    }
    public bool IsTimeIncrease
    {
        get { return isTimeIncrease; }
        set { isTimeIncrease = value; }
    }
    public bool IsBonusPoints
    {
        get { return isBonusPoints; }
        set { isBonusPoints = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isSpeedUp)
        {
            label.text = "S";
        }
        else if (isTimeIncrease)
        {
            label.text = "T";
        }
        else if (isBonusPoints)
        {
            label.text = "P";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player 1" && forPlayer1)
        {
            ApplyEffect(p1);
        }

        if (collision.gameObject.name == "Player 2" && !forPlayer1)
        {
            ApplyEffect(p2);
        }
    }

    void ApplyEffect(Player player)
    {
        if (isSpeedUp)
        {
            player.Speed += .02f;
            Destroy(this.gameObject);
        }

        else if (isTimeIncrease)
        {
            player.Timer += 30;
            Destroy(this.gameObject);
        }

        else if (isBonusPoints)
        {
            player.Score += 30;
            Destroy(this.gameObject);
        }
    }
}
