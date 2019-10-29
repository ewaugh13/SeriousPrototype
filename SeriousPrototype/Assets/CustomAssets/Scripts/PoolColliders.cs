using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolColliders : MonoBehaviour
{
    //public GameObject spawnObject;

    #region AttachCorals
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CoralStubs")
        {
            //Debug.Log("Hello");

            // Positioning
            //Vector3 spawnLocation = new Vector3();
            //spawnLocation.x = gameObject.transform.position.x;
            //spawnLocation.y = gameObject.transform.position.y + 0.05f;
            //spawnLocation.z = gameObject.transform.position.z;

            //// Rotation
            //Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);

            //// Scale
            ////spawnObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);

            //// Creation and Destruction
            //Instantiate(spawnObject, spawnLocation, spawnRotation);
            //Destroy(collision.gameObject);

            // Increase Count
            GameManager.s_numberOfCoralsInTray++;
        }

        // CheckTray
        CheckTray();
    }

    public void CheckTray()
    {
        if (GameManager.s_numberOfCoralsInTray == 4)
        {
            // Delay

            // Play Voiceover

            // Teleport the player
            Debug.Log("Teleporting");
            GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
            playerObject.transform.position = new Vector3 (-42.4f, 3.345f, 53.299f);
        }
    }
    #endregion
}
