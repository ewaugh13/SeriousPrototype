using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CoralDiscCollider : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Coral"))
        {
            // Positioning
            Vector3 spawnLocation = new Vector3();
            spawnLocation.x = gameObject.transform.position.x;
            spawnLocation.y = gameObject.transform.position.y + 0.02f;
            spawnLocation.z = gameObject.transform.position.z;

            // Rotation
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);

            // Creation and Destruction
            GameObject spawnedObject = Instantiate(collision.gameObject, spawnLocation, spawnRotation);
            spawnedObject.transform.localScale = collision.gameObject.GetComponent<CutCoralPiece>().getOriginalScale();
            spawnedObject.tag = "Untagged";

            // remove interaction from spawned object and set parent to disc
            RemoveInteractable(spawnedObject);
            spawnedObject.transform.parent = this.gameObject.transform;

            // remove interactable and the game object from the scene
            // interable removale is needed to unmap hand before we call delete
            RemoveInteractable(collision.gameObject);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);

            // TODO add all interactivity

            // ready to be placed in pool
            this.gameObject.tag = "CoralStubs";

            // turn of this component after adding coral piece
            this.enabled = false;






            //// Increasing the Count
            //GameManager.s_numberOfCoralStubs += 1;

            //// CheckStubs
            //CheckStubs();

            //collision.gameObject.isStatic = true;
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

    private void RemoveInteractable(GameObject interactableGameObject)
    {
        Destroy(interactableGameObject.GetComponent<BoxCollider>());
        Destroy(interactableGameObject.GetComponent<SteamVR_Skeleton_Poser>());
        Destroy(interactableGameObject.GetComponent<InteractableHoverEvents>());
        Destroy(interactableGameObject.GetComponent<Throwable>());
        Destroy(interactableGameObject.GetComponent<Interactable>());
        Destroy(interactableGameObject.GetComponent<Rigidbody>());
    }

    private void AddInteractable(GameObject newInteractableGameObject)
    {
        newInteractableGameObject.AddComponent<Rigidbody>();
        // TODO verify gravity is set
        newInteractableGameObject.AddComponent<BoxCollider>();
        newInteractableGameObject.AddComponent<Interactable>();
        newInteractableGameObject.AddComponent<Throwable>();
        // TODO make it so its not actually throwable
        newInteractableGameObject.AddComponent<InteractableHoverEvents>();
        // TODO add hover events
        newInteractableGameObject.AddComponent<SteamVR_Skeleton_Poser>();
    }
}
