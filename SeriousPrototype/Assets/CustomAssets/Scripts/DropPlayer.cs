using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;
    public AudioSource Station1intro = null;
    public GameObject playerObject;
    public GameObject labTeleport = null;

    // Movement speed in units per second.
    private float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;


    private IEnumerator WaitToTeleport(float timeToWait)
    {
        float elapsedTime = 0;
        while (elapsedTime < timeToWait)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameObject playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        playerObject.transform.position = labTeleport.transform.position;
        Station1intro.Play();
    }

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        StartCoroutine(DropPlayerCall());
        StartCoroutine(WaitToTeleport(43));
    }

    

    // Move to the target end position.
    void Update()
    {
    }

    public IEnumerator DropPlayerCall()
    {
        float fractionOfJourney = 0;
        while (fractionOfJourney < 1.0f)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            playerObject.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
            yield return null;
        }
    }
}
