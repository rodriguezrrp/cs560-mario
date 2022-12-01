using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    int coins = 0;
    int stars = 0;

    [SerializeField] Text coinsText;
    [SerializeField] Text starsText;

    [SerializeField] AudioSource collectionSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            coinsText.text = "Coins: " + coins;
            collectionSound.Play();
            PlayerLife playerLife = GetComponent<PlayerLife>();
            playerLife.Heal(1);
        }
        if (other.gameObject.CompareTag("Star"))
        {
            Destroy(other.gameObject);
            stars++;
            starsText.text = "Stars: " + stars;
            collectionSound.Play();
            // fully heal
            PlayerLife playerLife = GetComponent<PlayerLife>();
            playerLife.Heal(playerLife.maxHealth);
        }
    }
}