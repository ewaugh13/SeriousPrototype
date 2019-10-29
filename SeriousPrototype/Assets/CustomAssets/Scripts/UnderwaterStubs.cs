using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class UnderwaterStubs : MonoBehaviour
{
    //public GameObject spawnObject;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coral")
        {
            //Debug.Log("Hello");

            //// Positioning
            //Vector3 spawnLocation = new Vector3();
            //spawnLocation.x = gameObject.transform.position.x;
            //spawnLocation.y = gameObject.transform.position.y + 0.10f;
            //spawnLocation.z = gameObject.transform.position.z;

            //// Rotation
            //Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);

            //// Scale
            ////spawnObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);

            //// Creation and Destruction
            //Instantiate(spawnObject, spawnLocation, spawnRotation);
            //Destroy(collision.gameObject);

            // Make Parent
            Destroy(collision.gameObject.GetComponent<Throwable>());
            Destroy(collision.gameObject.GetComponent<Interactable>());

            collision.gameObject.transform.position = gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject);

            //collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }
}
