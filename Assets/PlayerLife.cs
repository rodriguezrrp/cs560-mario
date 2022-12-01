using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;

    bool dead = false;

    public int maxHealth = 8;
    public int health;
    float invulnTimer; // used for 'invulnerability frames'

    [SerializeField] Text healthText;

    private void Start()
    {
        health = maxHealth;
        healthText.text = "Health: " + health + " / " + maxHealth;
        invulnTimer = 1f;
    }

    private void Update()
    {
        if (invulnTimer > 0)
            invulnTimer -= Time.deltaTime;
        if (transform.position.y < -1f && !dead)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            Damage(2);
        }
    }

    public void Heal(int amt)
    {
        health += amt;
        if (health > maxHealth)
            health = maxHealth;
        healthText.text = "Health: " + health + " / " + maxHealth;
    }

    public void Damage(int amt)
    {
        if (invulnTimer > 0)
            return;  // fail damage if currently invulnerable
        health -= amt;
        invulnTimer = 1f;
        if (health < 0)
            health = 0;
        healthText.text = "Health: " + health + " / " + maxHealth;
        AttemptDie();
    }

    void AttemptDie()
    {
        if (health > 0)
            return;  // fail death if health is not actually at 0
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMovement>().enabled = false;
        Die();
    }

    void Die()
    {
        health = 0;
        healthText.text = "Health: " + health + " / " + maxHealth;
        deathSound.Play();
        Invoke(nameof(ReloadLevel), 3.0f);
        dead = true;
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}