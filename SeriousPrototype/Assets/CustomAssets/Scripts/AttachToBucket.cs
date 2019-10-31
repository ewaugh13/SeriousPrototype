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
        if (collision.gameObject.tag.Equals("Coral") && !collision.gameObject.name.Contains("Copy"))
        {
            GameManager.s_numCoralsInCutBucket++;
            if (GameManager.s_numCoralsInCutBucket == MaxCoralPieces)
            {
                Debug.Log("2 to 3 VOX Played");
                Station2EndAudio.Play();
            }
            string originalTag = collision.gameObject.tag;
            collision.gameObject.tag = "Untagged";
            collision.gameObject.transform.parent = thisBucket.gameObject.transform;
            collision.gameObject.GetComponent<Renderer>().material = collision.gameObject.GetComponent<CutCoralPiece>().getOriginalMaterial();

            GameObject copyCoral = Instantiate(collision.gameObject);
            copyCoral.name = "CopyCoral";
            copyCoral.GetComponent<CutCoralPiece>().setOriginalScale(collision.gameObject.GetComponent<CutCoralPiece>().getOriginalScale());

            Destroy(collision.gameObject.GetComponent<CutCoralPiece>());
            Destroy(collision.gameObject.GetComponent<Throwable>());
            Destroy(collision.gameObject.GetComponent<InteractableHoverEvents>());
            Destroy(collision.gameObject.GetComponent<Interactable>());
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;

            Material originalMaterial = copyCoral.GetComponent<CutCoralPiece>().getOriginalMaterial();
            Texture originalTexture = copyCoral.GetComponent<CutCoralPiece>().getOriginalMaterial().mainTexture;
            Material[] materials = copyCoral.GetComponent<MeshRenderer>().materials;

            for (int i = 0; i < materials.Length; i++)
            {
                if(materials[i].mainTexture != originalTexture)
                {
                    copyCoral.GetComponent<MeshRenderer>().materials[i].mainTexture = originalTexture;
                    copyCoral.GetComponent<Renderer>().material = originalMaterial;
                }
            }

            copyCoral.transform.parent = otherBucket.transform;
            copyCoral.transform.localScale = collision.gameObject.transform.localScale;
            copyCoral.transform.position = teleportPosition.position;
            copyCoral.tag = originalTag;

            collision.gameObject.GetComponent<BoxCollider>().enabled = true;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;

            copyCoral.GetComponent<BoxCollider>().enabled = true;
            copyCoral.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
