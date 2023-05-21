using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float shotLifetime = 2f;
    [SerializeField] float shotSpeed = 3f;
    [HideInInspector] public bool isShooting;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float timeShotsVariance = 0f;
    [SerializeField] float minTimeBetweenShots = 0.1f;

    Coroutine fireCoroutine;

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }
    }

void Update()
    {
        if (isShooting && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isShooting && fireCoroutine != null) 
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    IEnumerator FireContinuously() 
    {
            while (true)
            {
                Fire();
                float waitTime = Random.Range(timeBetweenShots - timeShotsVariance, timeBetweenShots + timeShotsVariance);
                waitTime = Mathf.Clamp(minTimeBetweenShots, timeBetweenShots, float.MaxValue);   
                yield return new WaitForSeconds(waitTime);
            }
    }
    void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        int laserDirection = 1;
        if (useAI)
        {
            laserDirection = -1;
        }
        laserRb.velocity = new Vector2(0, shotSpeed * laserDirection);

        Destroy(laser, shotLifetime);
    }

}
