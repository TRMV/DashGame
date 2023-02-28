using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public Rigidbody RB;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = -Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("KillZone"))
            Destroy(gameObject);
    }
}
