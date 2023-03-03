using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNewSpawn : MonoBehaviour
{
    
    public GameObject[] spawn_possible;
    
    private int security;
    private int randomizer;
    private int randoLR;

    // Start is called before the first frame update
    void Start()
    {
        randomizer = Random.Range(0, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            #region (prochaine tile : Commence par milieu)
            if (CompareTag("Right") || CompareTag("Left"))
            {
                if (randomizer == 0)
                {
                    if (security == 0)
                    {
                        Debug.Log("1");
                        GameObject newGameplay = Instantiate(spawn_possible[0]) as GameObject;
                        newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                        security += 1;
                    }
                }
                if (randomizer == 1)
                {
                    if (security == 0)
                    {
                        Debug.Log("4");
                        GameObject newGameplay = Instantiate(spawn_possible[3]) as GameObject;
                        newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                        security += 1;
                    }
                }
            }
            #endregion

            #region (Prochaine tile : Commence soit par la gauche soit par la droite)
            if (CompareTag("Milieu"))
            {
                randoLR = Random.Range(0, 3);
                if (randoLR == 0)
                {
                    if (randomizer == 0)
                    {
                        if (security == 0)
                        {
                            Debug.Log("2");
                            GameObject newGameplay = Instantiate(spawn_possible[1]) as GameObject;
                            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                            security += 1;
                        }
                    }
                    if (randomizer == 1)
                    {
                        if (security == 0)
                        {
                            Debug.Log("5");
                            GameObject newGameplay = Instantiate(spawn_possible[4]) as GameObject;
                            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                            security += 1;
                        }
                    }
                    if (randomizer == 2)
                    {
                        if (security == 0)
                        {
                            Debug.Log("7");
                            GameObject newGameplay = Instantiate(spawn_possible[6]) as GameObject;
                            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                            security += 1;
                        }
                    }
                }
                if (randoLR == 1)
                {
                    if (randomizer == 0)
                    {
                        if (security == 0)
                        {
                            Debug.Log("3");
                            GameObject newGameplay = Instantiate(spawn_possible[2]) as GameObject;
                            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                            security += 1;
                        }
                    }
                    if (randomizer == 1)
                    {
                        if (security == 0)
                        {
                            Debug.Log("6");
                            GameObject newGameplay = Instantiate(spawn_possible[5]) as GameObject;
                            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                            security += 1;
                        }
                    }
                    if (randomizer == 2)
                    {
                        if (security == 0)
                        {
                            Debug.Log("8");
                            GameObject newGameplay = Instantiate(spawn_possible[7]) as GameObject;
                            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + 112.5f);
                            security += 1;
                        }
                    }
                }

            }
            #endregion
        }
    }

}
