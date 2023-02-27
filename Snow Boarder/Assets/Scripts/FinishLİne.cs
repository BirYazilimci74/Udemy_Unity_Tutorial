using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLİne : MonoBehaviour
{
    [SerializeField] private ParticleSystem finishEffect;
    [SerializeField] private float delayAmount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene",delayAmount);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
