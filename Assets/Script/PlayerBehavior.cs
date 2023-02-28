using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public float pullForce;

    public float dashSpeed;
    public float dashLength;
    public int dashNumber = 3;
    public float dashCD;

    private int maxDash;
    private bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxDash = dashNumber;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();

        Dash();
    }

    public void Rotation()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 lookVector = Vector3.forward * ver + -Vector3.left * hor;

        if (hor != 0 && ver != 0)
        {
            transform.rotation = Quaternion.LookRotation(lookVector);
        }
    }

    public void Dash()
    {
        //gravité vers le bas
        if (!isDashing)
        {
            rb.velocity = new Vector3(0, 0, -pullForce);
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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
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

    }
}
