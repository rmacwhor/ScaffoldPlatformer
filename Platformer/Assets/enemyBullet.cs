using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float damage = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //make player take damage
        }

        if (collision.transform.root.gameObject.tag != "enemy")
        {
            Destroy(this.gameObject);
        }
        
    }
}
