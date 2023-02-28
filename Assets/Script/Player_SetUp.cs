using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SetUp : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
            Destroy(gameObject);
        if (other.gameObject.CompareTag("Ennemy"))
            Destroy(other.gameObject);
    }
}
