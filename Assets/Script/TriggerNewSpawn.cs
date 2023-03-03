using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNewSpawn : MonoBehaviour
{
    public GameObject[] spawn_possible;

    private int security;
    private int randomizer;
    private int randoLR;
    public float distance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            randomizer = Random.Range(0, 3);

            #region (prochaine tile : Commence par milieu)
            if (CompareTag("Right") || CompareTag("Left"))
            {
                if (randomizer == 0)
                {
                    Debug.Log("1");
                    Spawn(0);
                }
                if (randomizer == 1)
                {
                    Debug.Log("4");
                    Spawn(3);
                }
                if (randomizer == 2)
                {
                    Debug.Log("1");
                    Spawn(0);
                }
            }
            #endregion

            #region (Prochaine tile : Commence soit par la gauche soit par la droite)
            if (CompareTag("Milieu"))
            {
                randoLR = Random.Range(0, 2);
                if (randoLR == 0)
                {
                    if (randomizer == 0)
                    {
                        Debug.Log("2");
                        Spawn(1);
                    }
                    if (randomizer == 1)
                    {
                        Debug.Log("5");
                        Spawn(4);
                    }
                    if (randomizer == 2)
                    {
                        Debug.Log("7");
                        Spawn(6);
                    }
                }
                if (randoLR == 1)
                {
                    if (randomizer == 0)
                    {
                        Debug.Log("3");
                        Spawn(2);
                    }
                    else if (randomizer == 1)
                    {
                        Debug.Log("6");
                        Spawn(5);
                    }
                    else if (randomizer == 2)
                    {
                        Debug.Log("8");
                        Spawn(7);
                    }
                }

            }
            #endregion
        }
    }


    public void Spawn(int spawnnumber)
    {
        if (security == 0)
        {
            Debug.Log("Whoa !");
            GameObject newGameplay = Instantiate(spawn_possible[spawnnumber]);
            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
            security++;
        }
    }
}
