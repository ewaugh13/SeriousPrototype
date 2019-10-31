using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PoolColliders : MonoBehaviour
{
    public GameObject teleportPoint;

    #region AttachCorals
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("CoralStubs"))
        {
            //Debug.Log("Hello");

            // Positioning
            Vector3 spawnLocation = new Vector3();
            spawnLocation.x = gameObject.transform.position.x;
            spawnLocation.y = gameObject.transform.position.y;
            spawnLocation.z = gameObject.transform.position.z;

            // Rotation
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);

            // Scale
            collision.gameObject.transform.position = gameObject.transform.position;

            //// Creation and Destruction
            //Instantiate(spawnObject, spawnLocation, spawnRotation);
            //Destroy(collision.gameObject);
            RemoveInteractable(collision.gameObject);

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
            playerObject.transform.position = teleportPoint.transform.position;
        }
    }
    #endregion

    private void RemoveInteractable(GameObject interactableGameObject)
    {
        interactableGameObject.SetActive(false);
        Destroy(interactableGameObject.GetComponent<BoxCollider>());
        Destroy(interactableGameObject.GetComponent<SteamVR_Skeleton_Poser>());
        Destroy(interactableGameObject.GetComponent<InteractableHoverEvents>());
        Destroy(interactableGameObject.GetComponent<Throwable>());
        Destroy(interactableGameObject.GetComponent<Interactable>());
        Destroy(interactableGameObject.GetComponent<Rigidbody>());
        interactableGameObject.SetActive(true);
        //interactableGameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
