using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Coral"))
        {
            Destroy(this.gameObject);
        }
    }
}
