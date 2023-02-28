using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public PlayerBehavior PlayerScript;
    public GameObject player;

    public Image[] Ui_dash;
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
        }
        if (PlayerScript.dashNumber == 2)
        {
            Ui_dash[0].color = Color.gray;
            Ui_dash[1].color = Color.blue;
            Ui_dash[2].color = Color.blue;
        }
        if (PlayerScript.dashNumber == 1)
        {
            Ui_dash[0].color = Color.gray;
            Ui_dash[1].color = Color.gray;
            Ui_dash[2].color = Color.blue;
        }
        if (PlayerScript.dashNumber == 0)
        {
            Ui_dash[0].color = Color.gray;
            Ui_dash[1].color = Color.gray;
            Ui_dash[2].color = Color.gray;
        }

    }
}
