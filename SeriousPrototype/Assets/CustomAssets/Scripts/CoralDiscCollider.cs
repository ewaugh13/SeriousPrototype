using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralDiscCollider : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Coral"))
        {
            // Positioning
            Vector3 spawnLocation = new Vector3();
            spawnLocation.x = gameObject.transform.position.x;
            spawnLocation.y = gameObject.transform.position.y + 0.10f;
            spawnLocation.z = gameObject.transform.position.z;

            // Rotation
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);

            // Scale
            //spawnObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);

            // Creation and Destruction
            Instantiate(collision.gameObject, spawnLocation, spawnRotation);
            Destroy(collision.gameObject);

            // Increasing the Count
            GameManager.s_numberOfCoralStubs++;

            // CheckStubs
            CheckStubs();
        }
    }

    public void CheckStubs()
    {
        Debug.Log(GameManager.s_numberOfCoralStubs);

        if (GameManager.s_numberOfCoralStubs == GameManager.s_numberOfCoralDiscs)
        {
            // Delay
            
            // Play Voiceover

            // Teleport the player
            // GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        }
    }
}
