using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class AttachToBucket : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("The current bucket to add current coral pieces")]
    [SerializeField]
    private GameObject thisBucket = null;
    [Tooltip("The bucket to add other coral pieces")]
    [SerializeField]
    private GameObject otherBucket = null;
    [Tooltip("The position to teleport to")]
    [SerializeField]
    private Transform teleportPosition = null;
    [Tooltip("Audio Source to play at the end of station 2")]
    [SerializeField]
    private AudioSource Station2EndAudio = null;

    #endregion

    #region Hidden Variables
    private int MaxCoralPieces = 4;
    #endregion

    private void OnTriggerEnter(Collider collision)
    {
        // if its a coral piece we create a duplicate in the other bucket across the room
        if (collision.gameObject.tag.Equals("Coral"))
        {
            GameManager.s_numCoralsInCutBucket++;
            if (GameManager.s_numCoralsInCutBucket == MaxCoralPieces)
            {
                Station2EndAudio.Play();
            }

            // remove tag so we can no longer cut this piece as its in the bucket
            string originalTag = collision.gameObject.tag;
            collision.gameObject.tag = "Untagged";
            // set parenting of coral piece to the bucket
            collision.gameObject.transform.parent = thisBucket.gameObject.transform;
            // reset material of object put in bucket
            this.gameObject.GetComponent<RemoveHighlightedMaterial>().removeMaterial(collision.gameObject);

            // create the copy coral
            GameObject copyCoral = Instantiate(collision.gameObject);
            copyCoral.GetComponent<CutCoralPiece>().setOriginalScale(collision.gameObject.GetComponent<CutCoralPiece>().getOriginalScale());

            // make the dropped in coral piece no longer interactable
            Destroy(collision.gameObject.GetComponent<CutCoralPiece>());
            Destroy(collision.gameObject.GetComponent<Throwable>());
            Destroy(collision.gameObject.GetComponent<InteractableHoverEvents>());
            Destroy(collision.gameObject.GetComponent<Interactable>());
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;

            // reset the material in case vr highlighted material was active when put in bucket and cloned
            this.gameObject.GetComponent<RemoveHighlightedMaterial>().removeMaterial(copyCoral);

            // set the parent, position, scale and tag of the copied coral to be used later
            copyCoral.transform.parent = otherBucket.transform;
            copyCoral.transform.localScale = collision.gameObject.transform.localScale;
            copyCoral.transform.position = teleportPosition.position;
            copyCoral.tag = originalTag;

            // reinitalize collision and rigid body on coral piece at bottom of bucket
            collision.gameObject.GetComponent<BoxCollider>().enabled = true;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;

            // add collision and rigid body on copy coral piece at bottom of other bucket
            copyCoral.GetComponent<BoxCollider>().enabled = true;
            copyCoral.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
