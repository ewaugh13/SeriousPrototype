using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station3PVOXAfter1Coral : MonoBehaviour
{
    [SerializeField]
    private AudioSource Station3AfterCoral = null;

    private bool AudioPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coral") && AudioPlayed == false)
        {
            Station3AfterCoral.Play();
            AudioPlayed = true;
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
