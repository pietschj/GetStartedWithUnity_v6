using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI coinText;

    [Header("Level Complete")]
    [Tooltip("How many coins the player must collect to finish the level.")]
    [SerializeField] int coinsRequired = 10;
    [Tooltip("Panel shown when all coins are collected. Starts hidden.")]
    [SerializeField] GameObject levelCompletePanel;

    [Header("Celebration")]
    [Tooltip("Confetti prefab spawned in front of the camera when the level is cleared.")]
    [SerializeField] GameObject confettiEffect;
    [Tooltip("How far in front of the camera the confetti appears. Keep it further than the Level Complete canvas Plane Distance, and closer than the player.")]
    [SerializeField] float confettiDistance = 1.2f;
   
    int coins;
    bool levelComplete;

    /*
     alternatively use:
      void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }*/

    void Start()
    {
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
        UpdateText();
    }

    public void IncreaseScore()
    {
       
        coins++;
        UpdateText();

        if (!levelComplete && coins >= coinsRequired)
        {
            ShowLevelComplete();
        }
    }

    void UpdateText()
    {
        coinText.text = "Coins: " + coins + " / " + coinsRequired;
    }

    void ShowLevelComplete()
    {
        levelComplete = true;

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }

        // celebrate: the confetti uses unscaled time, so it keeps playing while the game is paused
        SpawnConfetti();

        // pause the game so the robot stops moving behind the panel
        Time.timeScale = 0f;

        // the third person controller locks the cursor, so free it to click the button
        StarterAssets.StarterAssetsInputs input = FindFirstObjectByType<StarterAssets.StarterAssetsInputs>();
        if (input != null)
        {
            input.cursorLocked = false;
            input.cursorInputForLook = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SpawnConfetti()
    {
        if (confettiEffect == null) return;

        // use the same camera the Level Complete canvas is drawn by, so the confetti lines up with it
        Camera cam = Camera.main;
        Canvas canvas = levelCompletePanel != null ? levelCompletePanel.GetComponentInParent<Canvas>() : null;
        if (canvas != null && canvas.worldCamera != null) cam = canvas.worldCamera;
        if (cam == null) return;

        // half the height of the screen at that distance, so we can find the bottom edge
        float halfHeight = confettiDistance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        Vector3 position = cam.transform.position
                         + cam.transform.forward * confettiDistance
                         - cam.transform.up * halfHeight;

        // a particle system fires along its local Z axis, so tip it to point up the screen
        Quaternion rotation = cam.transform.rotation * Quaternion.Euler(90f, 0f, 0f);

        Instantiate(confettiEffect, position, rotation);
    }
}
