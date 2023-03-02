using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public Rigidbody RB;
    public float speed;
    public float2 rotationSpeed;
    private float rotationS;

    
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        rotationS = UnityEngine.Random.Range(rotationSpeed.x, rotationSpeed.y);
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = -Vector3.forward * speed ;
        transform.Rotate(Vector3.forward * rotationS * Time.deltaTime, Space.Self);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("KillZone"))
            Destroy(gameObject);
    }
}
