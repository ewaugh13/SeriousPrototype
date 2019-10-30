using EzySlice;
using UnityEngine;

public class CutCoralPiece : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("Number of triangles in mesh needed for cutting")]
    [SerializeField]
    private int numTriangles = 40;
    [Tooltip("Original material of the coral piece")]
    [SerializeField]
    private Material originalMaterial = null;
    [Tooltip("Original scale of the model")]
    [SerializeField]
    private Vector3 originalScale = Vector3.one;
    #endregion

    private const string CUT_PLANE = "CutPlane";



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Scissors") && collision.gameObject.name.Contains("Clone"))
        {
            GameObject cutPlane = collision.gameObject.transform.Find(CUT_PLANE).gameObject;
            PlaneUsageExample planeExample = cutPlane.GetComponent<PlaneUsageExample>();
            Material cutMaterial = originalMaterial;
            Mesh objectMesh = this.gameObject.GetComponent<MeshFilter>().sharedMesh;
            if (objectMesh.triangles.Length > numTriangles)
            {
                SlicedHull hull = planeExample.SliceObject(this.gameObject, null);
                if (hull != null)
                {
                    if (hull.upperHull.triangles.Length > numTriangles && hull.lowerHull.triangles.Length > numTriangles)
                    {
                        GameObject piece = hull.CreateUpperHull(this.gameObject, cutMaterial);

                        GameObject coralClone = Instantiate(this.gameObject);
                        coralClone.transform.position = this.gameObject.transform.position;

                        coralClone.GetComponent<MeshFilter>().sharedMesh = piece.GetComponent<MeshFilter>().sharedMesh;
                        coralClone.GetComponent<MeshFilter>().mesh = piece.GetComponent<MeshFilter>().mesh;
                        coralClone.GetComponent<MeshRenderer>().materials = piece.GetComponent<MeshRenderer>().materials;
                        Destroy(piece);

                        Destroy(coralClone.GetComponent<BoxCollider>());
                        coralClone.AddComponent<BoxCollider>();
                        coralClone.GetComponent<Rigidbody>().isKinematic = false;
                        coralClone.GetComponent<Rigidbody>().useGravity = true;

                        GameObject piece2 = hull.CreateLowerHull(this.gameObject, cutMaterial);

                        this.gameObject.GetComponent<MeshFilter>().sharedMesh = piece2.GetComponent<MeshFilter>().sharedMesh;
                        this.gameObject.GetComponent<MeshFilter>().mesh = piece2.GetComponent<MeshFilter>().mesh;
                        this.gameObject.GetComponent<MeshRenderer>().materials = piece2.GetComponent<MeshRenderer>().materials;
                        Destroy(piece2);
                        Destroy(this.gameObject.GetComponent<BoxCollider>());
                        this.gameObject.AddComponent<BoxCollider>();

                    }
                }
            }
            Destroy(collision.gameObject);
        }
    }

    public Material getOriginalMaterial()
    {
        return this.originalMaterial;
    }

    public Vector3 getOriginalScale()
    {
        return this.originalScale;
    }

    public void setOriginalScale(Vector3 updatedOriginalScale)
    {
        this.originalScale = updatedOriginalScale;
    }
}
