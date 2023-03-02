using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public float pullForce;
    public int scoring;

    public float time;
    public float reloadtime;
    public float AtractAddition;

    public float dashSpeed;
    public float dashLength;
    public int dashNumber = 3;
    public float dashCD;
    private int maxDash;
    private bool isDashing;

    private GameObject mycam;
    private bool camCanFollow;

    private GameObject bg;
    private Material bgMat;

    private GameObject scoretext;
    public TextMeshProUGUI finalscoretext;
    private GameObject deathscreen;

    private bool isPaused;
    private GameObject pausescreen;

    public ParticleSystem dashPS;
    public ParticleSystem hitPS;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mycam = GameObject.Find("Main Camera");
        bg = GameObject.Find("Background");
        scoretext = GameObject.Find("ScorePlay");
        deathscreen = GameObject.Find("DeathScreen");
        pausescreen = GameObject.Find("PauseScreen");

        maxDash = dashNumber;

        deathscreen.SetActive(false);
        pausescreen.SetActive(false);
        bgMat = bg.GetComponent<Renderer>().material;
    }

    void Update()
    {    
        Rotation();
        Dash();
        Pause();
        Scoring();

        time += Time.deltaTime;

        if (time >= 30f)
        {
            reloadtime = reloadtime + Time.deltaTime;
            if (reloadtime >= 10f)
            {
                StartCoroutine(Atraction());
                reloadtime = 0f;
            }
        }

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

        if ((hor != 0 || ver != 0) && !isPaused)
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
                rb.velocity = new Vector3(0, 0, -pullForce * (3 + AtractAddition));
            } else
            {                
                rb.velocity = new Vector3(0, 0, -pullForce * (1 + AtractAddition));
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

    public void Pause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                pausescreen.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                pausescreen.SetActive(false);
                isPaused = false;
            }
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
            GameObject.Find("RestartButtonDeath").GetComponent<Button>().Select(); ;
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
        }
    }

    IEnumerator Particle(ParticleSystem hop, Transform pos)
    {
        ParticleSystem psps = Instantiate(hop, pos.position, pos.rotation);
        psps.Play();
        yield return new WaitForSeconds(2f);
        Destroy(psps);
    }

    IEnumerator Atraction()
    {
        yield return new WaitForSeconds(1f);
        AtractAddition += 0.1f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("triggerCamera"))
        {
            camCanFollow = false;
        }
    }
}
