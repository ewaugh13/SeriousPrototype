using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UnderwaterStubs : MonoBehaviour
{
    public GameObject playerObject;
    public AudioSource OGBG = null, Station6BG = null, Station6Intro = null;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Coral"))
        {
            // Make Parent
            collision.gameObject.GetComponent<Interactable>().hideHandOnAttach = false;
            // remove interactability
            RemoveInteractable(collision.gameObject);

            // fix material if highlighted material was last one set
            this.gameObject.GetComponent<RemoveHighlightedMaterial>().removeMaterial(collision.gameObject);

            // set position
            collision.gameObject.transform.position = this.gameObject.transform.position;

            // remove colliders and rigid body
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            collision.gameObject.SetActive(true);
            Destroy(this.gameObject);

            GameManager.s_underwaterCoralStubs++;

            // if true the game is over play final music and audio voice over
            if (GameManager.s_underwaterCoralStubs == 4)
            {
                OGBG.Stop();
                Station6BG.Play();
                Station6Intro.Play();
                // teleport player to final area (bad hardcoding)
                playerObject.transform.position = new Vector3(-234.91f, 3.345f, 53.299f);
            }
        }
    }

    private void RemoveInteractable(GameObject interactableGameObject)
    {
        interactableGameObject.SetActive(false);
        Destroy(interactableGameObject.GetComponent<BoxCollider>());
        Destroy(interactableGameObject.GetComponent<SteamVR_Skeleton_Poser>());
        Destroy(interactableGameObject.GetComponent<InteractableHoverEvents>());
        Destroy(interactableGameObject.GetComponent<Throwable>());
        Destroy(interactableGameObject.GetComponent<Interactable>());
        Destroy(interactableGameObject.GetComponent<Rigidbody>());
        interactableGameObject.SetActive(true);
    }
}
