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

    public GameObject bg;
    private Material bgMat;

    public GameObject scoretext;
    public TextMeshProUGUI finalscoretext;
    public GameObject deathscreen;

    public ParticleSystem dashPS;
    public ParticleSystem hitPS;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mycam = GameObject.Find("Main Camera");
        maxDash = dashNumber;

        deathscreen.SetActive(false);
        bgMat = bg.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {    
        Rotation();
        Dash();
        Scoring();

        /*if (isDashing)
        {
            float bgspeed = bgMat.GetFloat("_DashValueAdd") + 0.5f * Time.deltaTime;
            bgMat.SetFloat("_DashValueAdd", bgspeed);
            Debug.Log(bgspeed + "dash");
        }
        else
        {
            float bgspeed = bgMat.GetFloat("_DashValueAdd") + 0.01f * Time.deltaTime;
            bgMat.SetFloat("_DashValueAdd", bgspeed);
            Debug.Log(bgspeed + "notdash");
        }*/
    }

    private void LateUpdate()
    {
        CamControl();
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
        if (camCanFollow && isDashing && Input.GetAxis("Vertical") >= 0)
        {
            mycam.transform.position = new Vector3(mycam.transform.position.x, mycam.transform.position.y, transform.position.z);
        }
    }

    public void Scoring()
    {
        if ((int)transform.position.z > scoring)
        {
            scoring = (int)transform.position.z;
        }
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
                StartCoroutine(Particle(dashPS, transform));
            }
        }

        if (other.gameObject.CompareTag("Obstacles"))
        {
            StartCoroutine(Particle(hitPS, other.transform));
            Destroy(other.gameObject);
            rb.AddForce(-transform.forward * dashSpeed * 1.5f, ForceMode.Impulse);
            Handheld.Vibrate();

        }
    }

    IEnumerator Particle(ParticleSystem hop, Transform pos)
    {
        ParticleSystem psps = Instantiate(hop, pos.position, pos.rotation);
        psps.Play();
        yield return new WaitForSeconds(2f);
        Destroy(psps);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("triggerCamera"))
        {
            camCanFollow = false;
        }
    }
}
