using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public float pullForce;
    public int scoring;

    public bool hasShield;
    private GameObject shipMesh;
    public Material shipmatref;

    public float time;
    public float reloadtime;
    public float AtractAddition;

    public float dashSpeed;
    public float dashLength;
    public int dashNumber = 3;
    public float dashCD;
    private int maxDash;
    private bool isDashing;

    private int shieldRecharge;

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
    public ParticleSystem astePS;
    public ParticleSystem shieldPS;

    public AudioSource aS;
    public AudioClip dashAC;
    public AudioClip boostAC;
    public AudioClip deathAC;
    public AudioClip asteroidAC;
    public AudioClip shieldAC;

    public AudioSource mainAS;
    public AudioClip deathMusic;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mycam = GameObject.Find("Main Camera");
        bg = GameObject.Find("Background");
        scoretext = GameObject.Find("ScorePlay");
        deathscreen = GameObject.Find("DeathScreen");
        pausescreen = GameObject.Find("PauseScreen");
        shipMesh = GameObject.Find("SHip_prefab");

        hasShield = true;

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
        VolumeCam();;

        time += Time.deltaTime;

        if (time >= 5f)
        {
            reloadtime = reloadtime + Time.deltaTime;
            if (reloadtime >= 10f)
            {
                StartCoroutine(Atraction());
                reloadtime = 0f;
            }
        }

        if (isDashing)
        {
            float bgspeed = bgMat.GetFloat("_DashValueAdd") + 0.5f * Time.deltaTime;
            bgMat.SetFloat("_DashValueAdd", bgspeed);
        }
        else
        {
            float bgspeed = bgMat.GetFloat("_DashValueAdd") + 0.01f * Time.deltaTime;
            bgMat.SetFloat("_DashValueAdd", bgspeed);
        }
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
            ChangeColor(new Color(255f, 255f, 0f, 0.5f));

            rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            isDashing = true;
            dashNumber--;
            aS.PlayOneShot(dashAC);

            StartCoroutine(EndDash());
        }
    }

    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashLength);
        ChangeColor(new Color(0f, 0f, 0f, 1f));
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

    public void VolumeCam()
    {
        if (mycam.GetComponent<Volume>().profile.TryGet(out Vignette vignette))
        {

            if (rb.velocity.z < 0)
            {
                vignette.intensity.value += 0.05f * Time.deltaTime;
            }
            else
            {
                vignette.intensity.value -= 0.5f * Time.deltaTime;
            }

        }  
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
            Death();
        }

        if (other.gameObject.CompareTag("Ennemy"))
        {
            aS.PlayOneShot(boostAC);

            Destroy(other.gameObject);
            if (dashNumber < maxDash)
            {
                dashNumber++;
                StartCoroutine(Particle(dashPS, transform));
            } else
            {
                if (hasShield == false)
                {
                    shieldRecharge++;
                    GameObject.Find("UI_ShieldON").GetComponent<Image>().fillAmount += 1/3;

                    if (shieldRecharge == 3)
                    {
                        hasShield = true;
                        shieldRecharge = 0;
                        GameObject.Find("ShieldSphere").SetActive(true);
                        GameObject.Find("UI_ShieldON").GetComponent<Image>().fillAmount = 1;
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Obstacles"))
        {
            aS.PlayOneShot(asteroidAC);

            StartCoroutine(Particle(astePS, other.transform));
            Destroy(other.gameObject);
            rb.AddForce(-transform.forward * dashSpeed * 1.5f, ForceMode.Impulse);
            ChangeColor(new Color(255f, 0f, 0f, 0.5f));

            if (hasShield)
            {
                aS.PlayOneShot(shieldAC);

                hasShield = false;
                StartCoroutine(Particle(shieldPS, transform));
                GameObject.Find("ShieldSphere").SetActive(false);

                GameObject.Find("UI_ShieldON").GetComponent<Image>().fillAmount = 0;
            }
            else
            {
                Death();
            }
        }
    }

    public void ChangeColor(Color color)
    {
        int range = shipMesh.GetComponent<Renderer>().materials.Length;

        for (int m = 1; m < range; m++)
        {
            if (shipMesh.GetComponent<Renderer>().materials[m] = shipmatref)
            {
                shipMesh.GetComponent<Renderer>().materials[m].SetColor("_Color1", color);
            }
        }

    }

    public void Death()
    {
        aS.PlayOneShot(deathAC);
        mainAS.Stop();
        mainAS.clip = deathMusic;
        mainAS.loop = true;
        mainAS.Play();

        transform.GetComponent<AudioListener>().enabled = false;
        mycam.GetComponent<AudioListener>().enabled = true;

        scoretext.SetActive(false);
        finalscoretext.GetComponent<TextMeshProUGUI>().text = "You survived :\n" + scoring + "m";
        deathscreen.SetActive(true);
        GameObject.Find("RestartButtonDeath").GetComponent<Button>().Select();
        StartCoroutine(Particle(hitPS, transform));

        GameObject.Find("UI_DashBar").SetActive(false); 
        Destroy(GameObject.Find("UI_ShieldOFF"));
        Destroy(gameObject);
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
        AtractAddition += 0.5f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("triggerCamera"))
        {
            camCanFollow = false;
        }
    }
}
