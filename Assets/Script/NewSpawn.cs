using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawn : MonoBehaviour
{
    public int random;
    public GameObject[] Obstacle;
    
    public GameObject SpawnPoint;

    private int security = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (security == 0)
        {
            if (random == 0)
            {
                GameObject newDash = Instantiate(Obstacle[0]) as GameObject;
                newDash.transform.position = SpawnPoint.transform.position;
                security += 1;
            }
            if (random == 1)
            {
                GameObject newDash = Instantiate(Obstacle[1]) as GameObject;
                newDash.transform.position = SpawnPoint.transform.position;
                security += 1;
            }
        }
    }
}
