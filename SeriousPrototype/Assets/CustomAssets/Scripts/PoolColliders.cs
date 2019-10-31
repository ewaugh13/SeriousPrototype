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
    [Tooltip("Audio Source to play at the end of station 4")]
    [SerializeField]
    private AudioSource station4EndAudio = null;
    [Tooltip("Audio Source to play at the start of station 5")]
    [SerializeField]
    private AudioSource station5StartAudio = null;
    [Tooltip("Time to wait for the teleporting to the next area")]
    [SerializeField]
    private float timeToWaitForTeleport = 29;
    [Tooltip("Original material of the coral piece")]
    [SerializeField]
    private Material originalMaterial = null;

    [Tooltip("Prefab of great star coral")]
    [SerializeField]
    private GameObject greatStarCoral = null;
    [Tooltip("Prefab of stag horn coral")]
    [SerializeField]
    private GameObject stagHornCoral = null;
    [Tooltip("Prefab of mountainous star coral")]
    [SerializeField]
    private GameObject mountainousStarCoral = null;
    [Tooltip("Prefab of pillar coral")]
    [SerializeField]
    private GameObject pillarCoral = null;
    [Tooltip("Prefab of elk horn coral")]
    [SerializeField]
    private GameObject elkHornCoral = null;

    [Tooltip("Spawn Point of new coral")]
    [SerializeField]
    private Transform spawnPoint = null;
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


            Material[] materials = collision.gameObject.GetComponentInChildren<MeshRenderer>().materials;
            Texture originalTexture = originalMaterial.mainTexture;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].mainTexture != originalTexture)
                {
                    collision.gameObject.GetComponentInChildren<MeshRenderer>().materials[i].mainTexture = originalTexture;
                    collision.gameObject.GetComponentInChildren<Renderer>().material = originalMaterial;
                }
            }

            // Increase Count
            GameManager.s_numberOfCoralsInTray++;
            CheckTray();
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.gameObject.transform.eulerAngles = Vector3.zero;

            Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);
            for(int i = 0; i < collision.gameObject.transform.childCount; i++)
            {
                if (collision.gameObject.transform.GetChild(i).gameObject.name.Contains("GreatStarCoral"))
                {
                    Instantiate(greatStarCoral, spawnPoint.position, spawnRotation);
                }
                else if (collision.gameObject.transform.GetChild(i).gameObject.name.Contains("StaghornClumpCoral"))
                {
                    Instantiate(stagHornCoral, spawnPoint.position, spawnRotation);
                }
                else if(collision.gameObject.transform.GetChild(i).gameObject.name.Contains("PillarCoral"))
                {
                    Instantiate(pillarCoral, spawnPoint.position, spawnRotation);
                }
                else if(collision.gameObject.transform.GetChild(i).gameObject.name.Contains("MountainousStarCoral"))
                {
                    Instantiate(mountainousStarCoral, spawnPoint.position, spawnRotation);
                }
                else if(collision.gameObject.transform.GetChild(i).gameObject.name.Contains("ElkHornCoral"))
                {
                    Instantiate(elkHornCoral, spawnPoint.position, spawnRotation);
                }
            }
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
        station4EndAudio.Play();
        float elapsedTime = 0;
        while (elapsedTime < timeToWait)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        playerObject.transform.position = teleportPoint.position;
        station5StartAudio.Play();
    }
}
