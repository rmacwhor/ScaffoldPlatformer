using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public Rigidbody2D rb;
    private int shots = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * -speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameObject hit = hitInfo.transform.root.gameObject;
        if (hit.tag == "enemy" && hit.GetComponent<Patrol>() != null && hitInfo.gameObject.name.StartsWith("rocket") == false)
        {
            if (shots == 0)
                hit.GetComponent<Patrol>().TakeDamage(damage);
            shots += 1;
        }
        Destroy(gameObject);
        
    }
}
