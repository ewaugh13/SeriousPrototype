using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjexctToTree : MonoBehaviour
{
    #region Instance Variables
    private bool HasCoral;
    private float local_x;
    private float local_y;
    private float local_z;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        HasCoral = false;
        local_x = this.gameObject.GetComponent<Transform>().localPosition.x;
        local_y = this.gameObject.GetComponent<Transform>().localPosition.y;
        local_z = this.gameObject.GetComponent<Transform>().localPosition.z;
    }
    
    // Runs whenever a collision is detected in the node's capsule triger
    private void OnTriggerEnter(Collider other)
    {
        // ONLY EXECTUE IF THE NODE HAS NO CORAL
        if (HasCoral == false)
        {
            // CHECK IF THE COLLISION IS WITH A CORAL

            // CHECK IF THE CORAL IS RELEASED FROM THE PLAYER'S HAND

            // SNAP THE CORAL TO THE CAPSULE
            other.transform.position = transform.position;

            HasCoral = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
