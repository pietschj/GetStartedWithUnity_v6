
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    [SerializeField] float volume = 1f;
    [SerializeField] GameObject collectEffect;

    ScoreManager scoreManager;
    
    void Start()
    {
        //scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        //unity 6 way of finding object
        scoreManager = FindFirstObjectByType<ScoreManager>();
        // also possible:
        // searches the Canvas and its children.
        // GameObject.Find("Canvas").GetComponentInChildren<ScoreManager>(); 
        // breaks if someone renames the object.
        // GameObject.Find("scoreText").GetComponent<ScoreManager>(); works, 
        if (scoreManager == null)
        {
            Debug.LogWarning("No ScoreManager found in the scene.");
        } else
        {
            Debug.Log(scoreManager);
        }


           
    }

    // Update is called once per frame
    private void OnTriggerEnter (Collider other)
    {
        if (!other.CompareTag("Player")) return;
       
        if (scoreManager != null) {

            scoreManager.IncreaseScore();
            
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
        }
        //instantiate the prefab collect effect
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, collectEffect.transform.rotation);
        }

        gameObject.SetActive(false);

    }
}
