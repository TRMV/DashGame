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
                    Spawn(other.gameObject, 0);
                }
                if (randomizer == 1)
                {
                    Debug.Log("4");
                    Spawn(other.gameObject, 3);
                }
                if (randomizer == 2)
                {
                    Debug.Log("1");
                    Spawn(other.gameObject, 0);
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
                        Spawn(other.gameObject, 1);
                    }
                    if (randomizer == 1)
                    {
                        Debug.Log("5");
                        Spawn(other.gameObject, 4);
                    }
                    if (randomizer == 2)
                    {
                        Debug.Log("7");
                        Spawn(other.gameObject, 6);
                    }
                }
                if (randoLR == 1)
                {
                    if (randomizer == 0)
                    {
                        Debug.Log("3");
                        Spawn(other.gameObject, 2);
                    }
                    else if (randomizer == 1)
                    {
                        Debug.Log("6");
                        Spawn(other.gameObject, 5);
                    }
                    else if (randomizer == 2)
                    {
                        Debug.Log("8");
                        Spawn(other.gameObject, 7);
                    }
                }

            }
            #endregion
        }
    }


    public void Spawn(GameObject other, int spawnnumber)
    {
        if (security == 0)
        {
            Debug.Log("Whoa !");
            GameObject newGameplay = Instantiate(spawn_possible[7]);
            newGameplay.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z + distance);
            security++;
        }
    }
}
