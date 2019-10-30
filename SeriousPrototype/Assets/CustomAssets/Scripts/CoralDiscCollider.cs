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

            // Scale
            //spawnObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);

            // Creation and Destruction
            GameObject spawnedObject = Instantiate(collision.gameObject, spawnLocation, spawnRotation);
            spawnedObject.transform.localScale = new Vector3(1, 1, 1);
            spawnedObject.tag = "Untagged";
            RemoveInteractable(spawnedObject);
            spawnedObject.transform.parent = this.gameObject.transform;

            RemoveInteractable(collision.gameObject);
            Destroy(collision.gameObject);

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
}
