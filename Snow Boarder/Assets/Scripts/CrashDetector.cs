using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private PlayerCotroller playerCotroller;
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private AudioClip crashSFX;
    [SerializeField] private float delayAmount;
    
    private bool hasCrashed = false;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") && !hasCrashed)
        {
            hasCrashed = true;
            DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", delayAmount);
        }
    }

    private void DisableControls()
    {
        playerCotroller.canMove = false;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
