using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDroppedObject : MonoBehaviour
{
    #region Hidden Variables
    private Vector3 originalPosition = Vector3.zero;
    private Vector3 originalRotation = Vector3.zero;
    #endregion

    private float yDisplacement = 0.1f;

    private void Start()
    {
        originalPosition = this.gameObject.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.transform.position = new Vector3(originalPosition.x, originalPosition.y + yDisplacement, originalPosition.z);
            this.gameObject.transform.eulerAngles = originalRotation;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
