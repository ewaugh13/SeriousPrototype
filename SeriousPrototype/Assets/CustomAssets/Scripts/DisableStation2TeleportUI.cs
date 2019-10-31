using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableStation2TeleportUI : MonoBehaviour
{
    public GameObject UI_Canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(UI_Canvas);
        }
    }
}
