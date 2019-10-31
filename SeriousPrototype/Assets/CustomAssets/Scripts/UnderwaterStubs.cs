using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class UnderwaterStubs : MonoBehaviour
{
    public GameObject playerObject;
    public AudioSource OGBG = null, Station6BG = null, Station6Intro = null;

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
            collision.gameObject.GetComponent<Interactable>().hideHandOnAttach = false;
            Destroy(collision.gameObject.GetComponent<Throwable>());
            Destroy(collision.gameObject.GetComponent<InteractableHoverEvents>());
            Destroy(collision.gameObject.GetComponent<Interactable>());

            collision.gameObject.transform.position = gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            collision.gameObject.SetActive(true);
            Destroy(this.gameObject);

            GameManager.s_underwaterCoralStubs++;

            if (GameManager.s_underwaterCoralStubs == 4)
            {
                OGBG.Stop();
                Station6BG.Play();
                Station6Intro.Play();
                playerObject.transform.position = new Vector3(-234.91f, 3.345f, 53.299f);
            }

            //collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }
}
