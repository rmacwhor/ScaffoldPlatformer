using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Needed to create Image instances

//-////////////////////////////////////////////////////
///
/// PlayerHealth handles the life of the Player by:
/// -Specifing health amount and keeping track of current Health
/// -Getting access to the UI healthBar element and updating it base on health
/// -Contains methods that damage the player
/// -Handles when the playaer gets hurt
///
public class PlayerHealth : MonoBehaviour 
{
    public float maxHealth;
    public float currentHealth;
    public Image healthBar; //UI Bar
    public GameManager gameManager;
    private SpriteRenderer playerSprite;
    public CharacterController2D characterController2D;

    //-////////////////////////////////////////////////////
    ///
    void Start () {
        currentHealth = maxHealth; //At start of scene, player gets max health
        healthBar.fillAmount = currentHealth;
        playerSprite = GetComponent<SpriteRenderer>();
        characterController2D = GetComponent<CharacterController2D>();
    }
	
    //-////////////////////////////////////////////////////
    ///
    /// Deals damage to player base on specified amount and updates UI and stats
    ///
	public void TakeDamage(float damage)
    {
        if (!characterController2D.m_Immune && currentHealth > 0)
        {
            
            currentHealth -= damage;
            float health = currentHealth / maxHealth;
            // healthBar.fillAmount = health;
            if (currentHealth <= 0)      //If health goes to 0 or below, call GameOver in GameManager
            {
                Debug.Log("Game Over");
                gameManager.GameOver();
            }
        }

    }

    //-////////////////////////////////////////////////////
    ///
    /// Heals Damage to the player base on specified amount and updates the UI
    ///
    public void HealDamage(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) //If health goes above max health then cap it at max health
            currentHealth = maxHealth;

        float health = currentHealth / maxHealth;
        healthBar.fillAmount = health;
    }

    //-////////////////////////////////////////////////////
    ///
    /// ONTriggerEnter2D is called when another trigger collider hits any of the player's colliders
    ///
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HurtBox" && this.gameObject.transform.position.y - collision.gameObject.transform.position.y >= 0)
        {
            characterController2D.m_RigidBody2D.velocity = new Vector2(characterController2D.m_RigidBody2D.velocity.x, 25);

        }
        if (collision.gameObject.tag == "HitBox")
        {
            if (!characterController2D.m_Immune)
            {
                StartCoroutine(BlinkSprite());
                StartCoroutine(DamageState());
            }
        }
    }

    //-////////////////////////////////////////////////////
    ///
    /// Calls Take Damage, and makes Player Immune for a short interval before they can get hit again
    ///
    IEnumerator DamageState()
    {
        TakeDamage(1);
        characterController2D.SetPlayerImmune(true);
        yield return new WaitForSeconds(1f); //Time before they can get hit again
        characterController2D.SetPlayerImmune(false);

    }

    //-////////////////////////////////////////////////////
    ///
    /// Makes the player's sprite blink
    ///
    IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 8; ++i)
        {
            yield return new WaitForSeconds(.05f);

            if (playerSprite.enabled == true)
                playerSprite.enabled = false;

            else
                playerSprite.enabled = true;
        }
    }
}
