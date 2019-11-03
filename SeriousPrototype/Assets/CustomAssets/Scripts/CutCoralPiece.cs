using EzySlice;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CutCoralPiece : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("Number of triangles in mesh needed for cutting")]
    [SerializeField]
    private int numTriangles = 40;
    [Tooltip("Original scale of the model")]
    [SerializeField]
    private Vector3 originalScale = Vector3.one;
    [Tooltip("Original rotation of the model")]
    [SerializeField]
    private Vector3 originalRotation = Vector3.zero;
    #endregion

    private const string CUT_PLANE = "CutPlane";

    private void OnCollisionEnter(Collision collision)
    {
        // only cut with cloned scissor object that follows the original but isn't attached to the hand
        if (collision.gameObject.tag.Equals("Scissors") && collision.gameObject.name.Contains("Clone"))
        {
            Destroy(this.gameObject.GetComponent<ResetDroppedObject>());

            GameObject cutPlane = collision.gameObject.transform.Find(CUT_PLANE).gameObject;
            PlaneUsageExample planeExample = cutPlane.GetComponent<PlaneUsageExample>();
            Mesh objectMesh = this.gameObject.GetComponent<MeshFilter>().sharedMesh;

            Material originalMaterial = this.gameObject.GetComponent<SetCoralPieceOriginalMaterial>().originalMaterial;

            // only cut the object if it has enough triangles
            if (objectMesh.triangles.Length > numTriangles)
            {
                SlicedHull hull = planeExample.SliceObject(this.gameObject, null);
                if (hull != null)
                {
                    // only create the cut pieces if they have enough triangles
                    if (hull.upperHull.triangles.Length > numTriangles && hull.lowerHull.triangles.Length > numTriangles)
                    {
                        GameObject piece = hull.CreateUpperHull(this.gameObject, originalMaterial);

                        // remove 2nd and 3rd hover event from coral piece
                        this.gameObject.GetComponent<InteractableHoverEvents>().onAttachedToHand.RemoveAllListeners();
                        this.gameObject.GetComponent<InteractableHoverEvents>().onDetachedFromHand.RemoveAllListeners();

                        // create clone for the piece no longer attached to the hand
                        GameObject coralClone = Instantiate(this.gameObject);
                        coralClone.transform.position = this.gameObject.transform.position;

                        // set that clones mesh to the upper hull mesh
                        coralClone.GetComponent<MeshFilter>().sharedMesh = piece.GetComponent<MeshFilter>().sharedMesh;
                        coralClone.GetComponent<MeshFilter>().mesh = piece.GetComponent<MeshFilter>().mesh;
                        coralClone.GetComponent<MeshRenderer>().materials = piece.GetComponent<MeshRenderer>().materials;
                        // destroy object made by ezy slice
                        Destroy(piece);

                        Destroy(coralClone.GetComponent<BoxCollider>());
                        // add collider and rigid body to new cut piece not attached to hand
                        coralClone.AddComponent<BoxCollider>();
                        coralClone.GetComponent<Rigidbody>().isKinematic = false;
                        coralClone.GetComponent<Rigidbody>().useGravity = true;

                        GameObject piece2 = hull.CreateLowerHull(this.gameObject, originalMaterial);

                        // set original game object mesh to the lowerhull
                        this.gameObject.GetComponent<MeshFilter>().sharedMesh = piece2.GetComponent<MeshFilter>().sharedMesh;
                        this.gameObject.GetComponent<MeshFilter>().mesh = piece2.GetComponent<MeshFilter>().mesh;
                        this.gameObject.GetComponent<MeshRenderer>().materials = piece2.GetComponent<MeshRenderer>().materials;
                        // destroy object made by ezy slice
                        Destroy(piece2);

                        // recreate box collider to fit new mesh
                        Destroy(this.gameObject.GetComponent<BoxCollider>());
                        this.gameObject.AddComponent<BoxCollider>();

                    }
                }
            }
            Destroy(collision.gameObject);
        }
    }

    public Vector3 getOriginalScale()
    {
        return this.originalScale;
    }

    public Vector3 getOriginalRotation()
    {
        return this.originalRotation;
    }

    public void setOriginalScale(Vector3 updatedOriginalScale)
    {
        this.originalScale = updatedOriginalScale;
    }
}
