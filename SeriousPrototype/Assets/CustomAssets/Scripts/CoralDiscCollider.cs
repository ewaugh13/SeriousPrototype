using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CoralDiscCollider : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("The parent object")]
    [SerializeField]
    private GameObject parentObjectDisc = null;
    #endregion

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
            Quaternion spawnRotation = Quaternion.Euler(collision.gameObject.GetComponent<CutCoralPiece>().getOriginalRotation());

            // Creation and Destruction
            GameObject spawnedObject = Instantiate(collision.gameObject, spawnLocation, spawnRotation);

            Material originalMaterial = spawnedObject.GetComponent<CutCoralPiece>().getOriginalMaterial();
            Texture originalTexture = spawnedObject.GetComponent<CutCoralPiece>().getOriginalMaterial().mainTexture;
            Material[] materials = spawnedObject.GetComponent<MeshRenderer>().materials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].mainTexture != originalTexture)
                {
                    spawnedObject.GetComponent<MeshRenderer>().materials[i].mainTexture = originalTexture;
                    spawnedObject.GetComponent<Renderer>().material = originalMaterial;
                }
            }

            spawnedObject.transform.localScale = collision.gameObject.GetComponent<CutCoralPiece>().getOriginalScale();
            spawnedObject.tag = "Untagged";

            // remove interaction from spawned object and set parent to disc
            RemoveInteractable(spawnedObject);
            spawnedObject.transform.parent = parentObjectDisc.transform;

            // remove interactable and the game object from the scene
            // interable removale is needed to unmap hand before we call delete
            RemoveInteractable(collision.gameObject);
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);

            // ready to be placed in pool
            parentObjectDisc.tag = "CoralStubs";

            parentObjectDisc.GetComponent<BoxCollider>().enabled = true;
            parentObjectDisc.GetComponent<Rigidbody>().useGravity = true;

            // destroy the collision point
            Destroy(this.gameObject);
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
