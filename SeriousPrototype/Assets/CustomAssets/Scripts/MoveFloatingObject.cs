using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloatingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += new Vector3(Time.deltaTime / 20, 0, 0);
    }
}
