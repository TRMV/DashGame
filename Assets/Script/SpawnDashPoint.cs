using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDashPoint : MonoBehaviour
{
    public int random;
    public GameObject[] spawnpoint;
    public GameObject Spawnable;
    public int security;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Randomizer());
    }

    // Update is called once per frame
    void Update()
    {
        if (security == 0)
        {
            GameObject Dash = Instantiate(Spawnable) as GameObject;
            Dash.transform.position = spawnpoint[random].transform.position;
            security = 1;
        }
    }

    IEnumerator Randomizer()
    {
        yield return new WaitForSeconds(5f);
        security = 0;
        random = Random.Range(0, 5);
        StartCoroutine(Randomizer());
    }
}
