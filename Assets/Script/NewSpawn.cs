using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawn : MonoBehaviour
{
    public GameObject Obstacle;
    
    public GameObject SpawnPoint;

    private int security = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (security == 0)
        {
            GameObject newDash = Instantiate(Obstacle) as GameObject;
            newDash.transform.position = SpawnPoint.transform.position;
            security += 1;
        }
    }
}
