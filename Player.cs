using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;

    public float shootingDelay = 0.2f;
    public bool shootingDelayed;

    public GameObject projectile;
    public Transform playerShip;
    public AudioSource gunAudio;

    public ScreenBounds screenBounds;

    // Update is called once per frame
    void Update()
    {
        //get input and move
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();
        Vector3 velocity = speed * input;
        Vector3 tempPosition = transform.localPosition + velocity * Time.deltaTime;
        if (screenBounds.AmIOutOfBounds(tempPosition))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(tempPosition);
            transform.position = newPosition;
        }
        else
        {
            transform.position = tempPosition;
        }
        

        //shooting
        if (Input.GetKey(KeyCode.Space))
        {
            if(shootingDelayed == false)
            {
                shootingDelayed = true;
                gunAudio.Play();
                GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
                StartCoroutine(DelayShooting());
            }
        }
    }

    private IEnumerator DelayShooting()
    {
        yield return new WaitForSeconds(shootingDelay);
        shootingDelayed = false;
    }

}
