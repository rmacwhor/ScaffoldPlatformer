using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    public float bulletVelocity;
    public Vector2 timeBetweenShots = new Vector2(1f, 2f);
    private float currentMaxTime = 0f;
    private float currentTime = 0f;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        currentMaxTime = UnityEngine.Random.Range(timeBetweenShots.x, timeBetweenShots.y);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= currentMaxTime)
        {
            canShoot = true;
            currentMaxTime = UnityEngine.Random.Range(timeBetweenShots.x, timeBetweenShots.y);
            currentTime = 0f;
        }
        if (canShoot && gameObject.GetComponent<Patrol>().inRange())
        {
            Shoot();
        }
        
    }
    void Shoot()
    {
        print("shooting");
        canShoot = false;
    }
}
