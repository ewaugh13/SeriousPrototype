using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SnapObjexctToTree : MonoBehaviour
{
    #region Hidden Variables
    private bool HasCoral;
    private float local_x;
    private float local_y;
    private float local_z;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        HasCoral = false;

        local_x = this.transform.localPosition.x;
        local_y = this.transform.localPosition.y;
        local_z = this.transform.localPosition.z;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coral") && HasCoral == false)
        {
            other.gameObject.transform.position = this.transform.position;
            //other.gameObject.transform.position -= new Vector3(0, 0.10f, 0);
            other.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            HasCoral = true;
            this.enabled = false;
            other.gameObject.isStatic = true;
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.isKinematic = true;

            var node = this.gameObject.GetComponent<MeshRenderer>();
            node.enabled = false;

            var script = other.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>();
            script.enabled = false;

            var script1 = other.gameObject.GetComponent<Valve.VR.SteamVR_Skeleton_Poser> ();
            script1.enabled = false;

            var script2 = other.gameObject.GetComponent<Valve.VR.InteractionSystem.Throwable>();
            script2.enabled = false;

            var script3 = other.gameObject.GetComponent<InteractableHoverEvents>();
            script3.enabled = false;


            Debug.Log(other.transform);
            //other.attachedRigidbody.freezeRotation = true;
            //Debug.Log(other.gameObject.transform.position);

        }
    }
}
