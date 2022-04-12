using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // variables
    [SerializeField] Player p1, p2;
    [SerializeField] Customer[] customers = new Customer[5];
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI p1Text, p2Text, tieText;
    [SerializeField] TextMeshProUGUI[] topScoresText = new TextMeshProUGUI[10];
    int customerCount = 0;
    public List<int> topScores = new List<int>(10);
    bool sorted = false;

    // properties
    public int CustomerCount
    {
        get { return customerCount; }
        set { customerCount = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!LoadGame())
        {
            GenerateTopScores();
        }
        AddCustomer(Random.Range(0, 5));
    }

    // Update is called once per frame
    void Update()
    {
        // adds new customer if none are active
        if (customerCount == 0)
        {
            AddCustomer(Random.Range(0, 5));
        }

        // checks to see if the game is over
        if (p1.GameOver && p2.GameOver)
        {
            GameOver();
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
        customers[index].Initialize();
        customers[index].gameObject.SetActive(true);
        customerCount++;
        StartCoroutine(NewCustomer());
    }

    void GameOver()
    {
        SortTopScore();
        gameOverPanel.SetActive(true);

        if (p1.Score > p2.Score)
        {
            p1Text.gameObject.SetActive(true);
        }
        else if (p1.Score < p2.Score)
        {
            p2Text.gameObject.SetActive(true);
        }
        else
        {
            tieText.gameObject.SetActive(true);
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GenerateTopScores()
    {
        int incr = 100;
        for (int i = 0; i < topScores.Count; i++)
        {
            topScores[i] += incr;
            incr += 100;
            topScoresText[i].text = topScores[i].ToString();
        }       
    }

    void SortTopScore()
    {
        if (!sorted)
        {
            topScores.Add(p1.Score);
            topScores.Add(p2.Score);
            topScores.Sort();
            topScores.RemoveAt(0);
            topScores.RemoveAt(0);

            for (int i = 0; i < topScores.Count; i++)
            {
                topScoresText[i].text = topScores[i].ToString();
            }
            sorted = true;
        }
        SaveGame();
    }

    // Save and Load Functions
    Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.scores = topScores;
        return save;
    }

    public void SaveGame()
    {
        // Create a Save instance with all the data for the current session saved into it
        Save save = CreateSaveGameObject();

        // Create a BinaryFormatter and a FileStream by passing a path for the Save instance to be saved to
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public bool LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // creates a save object by reading data from the file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            topScores = save.scores;
            return true;
        }
        else
        {
            return false;
        }
    }
}
