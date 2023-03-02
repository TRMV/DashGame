using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore_Script : MonoBehaviour
{
    
    public TextMeshPro HighScore;

    public PlayerBehavior Scoring;
    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        Scoring = GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void High_Score()
    {
        int number = Scoring.scoring;

        if (number > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.GetInt("HighScore", number);

    }
}
