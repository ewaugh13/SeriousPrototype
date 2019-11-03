using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStation3StartVOX : MonoBehaviour
{
    private bool PlayerOnStation = false;

    [SerializeField]
    private AudioSource Station3StartAudio = null;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player");
        if (other.gameObject.tag.Equals("Player") && PlayerOnStation == false)
        {
            PlayerOnStation = true;
            Station3StartAudio.Play();
        }
    }
}
