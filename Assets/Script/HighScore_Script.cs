using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore_Script : MonoBehaviour
{
    public TextMeshProUGUI HighScore;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        High_Score();
    }

    private void High_Score()
    {
        if (player)
        {
            int number = player.GetComponent<PlayerBehavior>().scoring;

            if (number > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", number);
                HighScore.text = "HighScore :\n" + number.ToString() + "m"; 
                
            }

            NewHS(number);
        }
    }
    private void NewHS(int number)
    {
        int New = number;

        if (number > PlayerPrefs.GetInt("HighScore", New))
        {
            PlayerPrefs.SetInt("HighScore", number);
            HighScore.text = "HighScore :\n" + number.ToString() + "m";
            New = number;
        }
    }
}
