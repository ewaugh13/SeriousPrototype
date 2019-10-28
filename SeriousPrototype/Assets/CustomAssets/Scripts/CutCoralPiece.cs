using EzySlice;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CutCoralPiece : MonoBehaviour
{
    private const string CUT_PLANE = "CutPlane";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Scissors") && collision.gameObject.name.Contains("Clone"))
        {
            GameObject cutPlane = collision.gameObject.transform.Find(CUT_PLANE).gameObject;
            PlaneUsageExample planeExample = cutPlane.GetComponent<PlaneUsageExample>();
            Material cutMaterial = this.gameObject.GetComponent<Renderer>().material;
            SlicedHull hull = planeExample.SliceObject(this.gameObject, null);
            if (hull != null)
            {
                GameObject piece1 = hull.CreateLowerHull(this.gameObject, cutMaterial);
                addNecessaryComponents(piece1);

                GameObject piece2 = hull.CreateUpperHull(this.gameObject, cutMaterial);
                addNecessaryComponents(piece2);

                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }

    private void addNecessaryComponents(GameObject piece)
    {
        piece.AddComponent<BoxCollider>();
        piece.AddComponent<Rigidbody>();

        piece.transform.rotation = this.gameObject.transform.rotation;
        if(this.gameObject.transform.parent != null && this.gameObject.transform.parent.name.Contains("Hand"))
        {
            piece.transform.position = this.gameObject.transform.parent.transform.position;
        }
        else
        {
            piece.transform.parent = this.gameObject.transform.parent;
            piece.transform.position = this.gameObject.transform.position;
        }

        // copy over steam vr interactable stuff
        Interactable interactable = this.gameObject.GetComponent<Interactable>();
        Interactable newInteractable = piece.AddComponent<Interactable>();
        newInteractable.hideHandOnAttach = interactable.hideControllerOnAttach;
        newInteractable.hideSkeletonOnAttach = interactable.hideSkeletonOnAttach;
        newInteractable.hideControllerOnAttach = interactable.hideControllerOnAttach;
        newInteractable.handAnimationOnPickup = interactable.handAnimationOnPickup;
        newInteractable.useHandObjectAttachmentPoint = interactable.useHandObjectAttachmentPoint;
        newInteractable.attachEaseIn = interactable.attachEaseIn;
        newInteractable.snapAttachEaseInTime = interactable.snapAttachEaseInTime;
        newInteractable.snapAttachEaseInCompleted = interactable.snapAttachEaseInCompleted;
        newInteractable.handFollowTransform = interactable.handFollowTransform;
        newInteractable.highlightOnHover = interactable.highlightOnHover;

        // TODO add the rest of components
        // Set up tag properly
        // See why object might fall through table
        // Possibly change names?
    }
}
