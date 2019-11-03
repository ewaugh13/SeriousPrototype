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
        // if hand collides with the boat object
        if(collision.gameObject.name.Contains("Hand"))
        {
            GameObject playerObject = null;
            foreach (GameObject playerTaggedObj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (playerTaggedObj.name.Equals("Player"))
                {
                    playerObject = playerTaggedObj;
                }
            }
            this.gameObject.GetComponent<DropPlayer>().DropPlayerStart();
            startAudio.Play();
        }
    }
}
