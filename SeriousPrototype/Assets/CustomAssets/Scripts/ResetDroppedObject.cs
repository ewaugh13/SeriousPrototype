using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDroppedObject : MonoBehaviour
{
    #region Hidden Variables
    private Vector3 originalPosition = Vector3.zero;
    private Vector3 originalRotation = Vector3.zero;
    #endregion

    private void Start()
    {
        originalPosition = this.gameObject.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            this.gameObject.transform.position = originalPosition;
            this.gameObject.transform.eulerAngles = originalRotation;
        }
    }
}
