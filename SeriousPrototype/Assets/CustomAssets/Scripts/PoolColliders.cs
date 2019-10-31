using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PoolColliders : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("The action for squeezing scissors")]
    [SerializeField]
    private Transform teleportPoint = null;
    [Tooltip("Audio Source to play at the end of station 3")]
    [SerializeField]
    private AudioSource station3EndAudio = null;
    [Tooltip("Time to wait for the teleporting to the next area")]
    [SerializeField]
    private float timeToWaitForTeleport = 29;
    [Tooltip("Original material of the coral piece")]
    [SerializeField]
    private Material originalMaterial = null;
    #endregion

    #region AttachCorals
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("CoralStubs"))
        {
            // Scale
            collision.gameObject.transform.position = gameObject.transform.position;

            //// Creation and Destruction
            //Instantiate(spawnObject, spawnLocation, spawnRotation);
            //Destroy(collision.gameObject);
            RemoveInteractable(collision.gameObject);

            Material[] materials = collision.gameObject.GetComponent<MeshRenderer>().materials;
            Texture originalTexture = originalMaterial.mainTexture;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].mainTexture != originalTexture)
                {
                    collision.gameObject.GetComponent<MeshRenderer>().materials[i].mainTexture = originalTexture;
                    collision.gameObject.GetComponent<Renderer>().material = originalMaterial;
                }
            }

            // Increase Count
            GameManager.s_numberOfCoralsInTray++;
            CheckTray();
            Destroy(this);
        }
    }

    public void CheckTray()
    {
        if (GameManager.s_numberOfCoralsInTray == 4)
        {
            // Teleport the player and play audio source
            StartCoroutine(waitToTeleport(timeToWaitForTeleport));
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
    }

    private IEnumerator waitToTeleport(float timeToWait)
    {
        station3EndAudio.Play();
        float elapsedTime = 0;
        while (elapsedTime < timeToWait)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        playerObject.transform.position = teleportPoint.position;
    }
}
