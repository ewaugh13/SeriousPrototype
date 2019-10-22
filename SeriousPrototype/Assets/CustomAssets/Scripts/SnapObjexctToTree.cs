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


    private void OnTriggerEnter(Collider other)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        HasCoral = false;

        local_x = this.transform.localPosition.x;
        local_y = this.transform.localPosition.y;
        local_z = this.transform.localPosition.z;
        //CapsuleCollider[] components = gameObject.GetComponents<CapsuleCollider>();
        //components[0].enabled
        //foreach (Component component in components)
        //{
        //    var type = component.GetType();
        //    Debug.Log(type);
        //    if (type.ToString() == "UnityEngine.CapsuleCollider")
        //    {

        //    }

        //}


    }

    // Update is called once per frame
    void Update()
    {

    }
}
