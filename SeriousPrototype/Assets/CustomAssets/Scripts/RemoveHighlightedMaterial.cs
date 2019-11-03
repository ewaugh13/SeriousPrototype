using UnityEngine;

public class RemoveHighlightedMaterial : MonoBehaviour
{
    public void removeMaterial(GameObject objectToRemoveMaterial)
    {
        Material originalMaterial = objectToRemoveMaterial.GetComponent<SetCoralPieceOriginalMaterial>().originalMaterial;
        Texture originalTexture = originalMaterial.mainTexture;

        for (int i = 0; i < objectToRemoveMaterial.GetComponent<MeshRenderer>().materials.Length; i++)
        {
            if (objectToRemoveMaterial.GetComponent<MeshRenderer>().materials[i].mainTexture != originalTexture
                || objectToRemoveMaterial.GetComponent<MeshRenderer>().materials[i] != originalMaterial)
            {
                objectToRemoveMaterial.GetComponent<Renderer>().material = originalMaterial;
                objectToRemoveMaterial.GetComponent<MeshRenderer>().materials[i] = originalMaterial;
                objectToRemoveMaterial.GetComponent<MeshRenderer>().materials[i].mainTexture = originalTexture;
            }
        }
    }
}
