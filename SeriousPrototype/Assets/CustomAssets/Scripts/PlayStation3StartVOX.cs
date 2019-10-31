using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStation3StartVOX : MonoBehaviour
{
    private bool PlayerOnStation = false;

    [SerializeField]
    private AudioSource Station3StartAudio = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag.Equals("Player"));
        if (other.gameObject.tag.Equals("Player") && PlayerOnStation == false)
        {
            PlayerOnStation = true;
            Station3StartAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
