using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralDiscCollider : MonoBehaviour
{
    public GameObject spawnObject;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Coral")
        {
            Debug.Log("Hello");

            Vector3 spawnLocation = new Vector3();
            spawnLocation.x = collision.gameObject.transform.position.x;
            spawnLocation.y = collision.gameObject.transform.position.y;
            spawnLocation.z = collision.gameObject.transform.position.z;

            Quaternion spawnRotation = collision.gameObject.transform.rotation;

            Instantiate(spawnObject, spawnLocation, spawnRotation);
            Destroy(collision.gameObject);
        }
    }
}
