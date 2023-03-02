using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public float pullForce;
    public int scoring;

    public float dashSpeed;
    public float dashLength;
    public int dashNumber = 3;
    public float dashCD;
    private int maxDash;
    private bool isDashing;

    public GameObject mycam;
    private bool camCanFollow;

    public Material bgMat;

    public GameObject scoretext;
    public TextMeshProUGUI finalscoretext;
    public GameObject deathscreen;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mycam = GameObject.Find("Main Camera");
        maxDash = dashNumber;

        deathscreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {    

        Rotation();
        Dash();
        Scoring();

        if (camCanFollow)
        {
            CamControl();
        }

        /*if (isDashing && bgMat.GetVector("_StarsScrollSpeed").y < 0.05f)
        {
            bgMat.SetVector("_StarsScrollSpeed", new Vector2(0.005f, bgMat.GetVector("_StarsScrollSpeed").y += 0.01f));
        }
        else if (!isDashing && bgMat.GetVector("_StarsScrollSpeed").y > 0.01f)
        {
            bgMat.SetVector("_StarsScrollSpeed", new Vector2(0.005f, 0.01f));
        }*/
    }

    public void Rotation()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 lookVector = Vector3.forward * ver + -Vector3.left * hor;

        if (hor != 0 || ver != 0)
        {
            transform.rotation = Quaternion.LookRotation(lookVector);
        }
    }

    public void Dash()
    {
        //gravité vers le bas
        if (!isDashing)
        {
            if (dashNumber == 0)
            {
                rb.velocity = new Vector3(0, 0, -pullForce * 3);
            } else
            {
                rb.velocity = new Vector3(0, 0, -pullForce);
            }
        }

        //dash
        if (Input.GetButtonDown("Dash") && !isDashing && dashNumber != 0)
        {
            rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            isDashing = true;
            dashNumber--;

            StartCoroutine(EndDash());
        }
    }

    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
    }

    public void CamControl()
    {
        if (isDashing && Input.GetAxis("Vertical") >= 0)
        {
            mycam.transform.position = new Vector3(mycam.transform.position.x, mycam.transform.position.y, transform.position.z);
        }
    }

    public void Scoring()
    {
        scoring = (int)transform.position.z;
        scoretext.GetComponent<TextMeshProUGUI>().text = scoring + "m";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("triggerCamera"))
        {
            //other.transform.position += new Vector3(0, 0, 3.5f);
            camCanFollow = true;
        }

        if (other.gameObject.CompareTag("KillZone"))
        {
            scoretext.SetActive(false);
            finalscoretext.GetComponent<TextMeshProUGUI>().text = "You survived :\n" + scoring + "m";
            deathscreen.SetActive(true);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Ennemy"))
        {
            Destroy(other.gameObject);
            if (dashNumber < maxDash)
            {
                dashNumber++;
            }
        }

        if (other.gameObject.CompareTag("Obstacles"))
        {
            Destroy(other.gameObject);
            rb.AddForce(transform.forward * -dashSpeed * 1f, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("triggerCamera"))
        {
            camCanFollow = false;
        }
    }
}
