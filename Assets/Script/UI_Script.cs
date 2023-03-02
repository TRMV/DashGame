using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Script : MonoBehaviour
{
    public PlayerBehavior PlayerScript;
    public GameObject player;

    public Image[] Ui_dash;

    public GameObject[] Ui_dashJauge;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = player.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.dashNumber == 3)
        {
            Ui_dash[0].color = Color.blue;
            Ui_dash[1].color = Color.blue;
            Ui_dash[2].color = Color.blue;
            Ui_dashJauge[0].SetActive(true); 
            Ui_dashJauge[1].SetActive(true);
            Ui_dashJauge[2].SetActive(true);
        }
        if (PlayerScript.dashNumber == 2)
        {
            Ui_dash[0].color = Color.gray;
            Ui_dash[1].color = Color.blue;
            Ui_dash[2].color = Color.blue;
            Ui_dashJauge[0].SetActive(false);
            Ui_dashJauge[1].SetActive(true);
            Ui_dashJauge[2].SetActive(true);
        }
        if (PlayerScript.dashNumber == 1)
        {
            Ui_dash[0].color = Color.gray;
            Ui_dash[1].color = Color.gray;
            Ui_dash[2].color = Color.blue;
            Ui_dashJauge[0].SetActive(false);
            Ui_dashJauge[1].SetActive(false);
            Ui_dashJauge[2].SetActive(true);
        }
        if (PlayerScript.dashNumber == 0)
        {
            Ui_dash[0].color = Color.gray;
            Ui_dash[1].color = Color.gray;
            Ui_dash[2].color = Color.gray;
            Ui_dashJauge[0].SetActive(false);
            Ui_dashJauge[1].SetActive(false);
            Ui_dashJauge[2].SetActive(false);
        }

    }
}
