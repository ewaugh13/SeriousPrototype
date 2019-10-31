﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableStation2TeleportUI : MonoBehaviour
{
    public GameObject UI_Canvas;
    private GameObject otherObj;
    public AudioSource Station_2_IntroAudio;
    public GameObject coralAudioInfo1, coralAudioInfo2, coralAudioInfo3, coralAudioInfo4, coralAudioInfo5;
    private bool atStation = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        otherObj = other.gameObject;
        if(other.CompareTag("Coral") && atStation == false)
        {
            Destroy(UI_Canvas);
            Destroy(coralAudioInfo1);
            Destroy(coralAudioInfo2);
            Destroy(coralAudioInfo3);
            Destroy(coralAudioInfo4);
            Destroy(coralAudioInfo5);
            Debug.Log("UI Canvas Destroyed");
            Station_2_IntroAudio.Play();
            atStation = true;
        }
    }

    private void Update()
    {
    }

}
