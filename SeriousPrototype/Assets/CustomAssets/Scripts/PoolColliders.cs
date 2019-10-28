using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolColliders : MonoBehaviour
{
    #region AttachCorals
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CoralStubs")
        {
            if(GameManager.s_numberOfCoralsInTray == 16)
            {
                // Delay

                // Play Voiceover

                // Teleport the player
                GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
                playerObject.transform.position = Vector3.zero;
            }
            GameManager.s_numberOfCoralsInTray++;

            // Junk Code
            //collision.gameObject.transform.parent = gameObject.transform;
            //Debug.Log("Collision GameObject : " + collision.gameObject);
            //Debug.Log("Tag : " + collision.gameObject.tag);
            //Debug.Log("GameObject : " + gameObject);
            //collision.gameObject.transform.position = gameObject.transform.position;
            //collision.gameObject.isStatic = true;
            //collision.transform.SetParent(gameObject.transform, false);
            //gameObject.scene.GetRootGameObjects()[0].transform.localPosition = Vector3.zero;
        }
    }
    #endregion
}
