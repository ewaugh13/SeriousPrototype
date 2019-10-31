using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToStartBoat : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("The audio at the beginning of the game")]
    [SerializeField]
    private AudioSource startAudio = null;
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Hand"))
        {
            GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
            this.gameObject.GetComponent<DropPlayer>().DropPlayerStart();
            startAudio.Play();
        }
    }
}
