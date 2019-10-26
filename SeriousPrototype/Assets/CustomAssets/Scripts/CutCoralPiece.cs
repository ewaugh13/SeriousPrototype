using UnityEngine;

public class CutCoralPiece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.enabled = false;
        foreach(ContactPoint contact in collision.contacts)
        {
            Debug.Log("X:" + contact.point.x + ", Y:" + contact.point.y + ", Z:" + contact.point.z);
        }
        Collider collider = collision.collider;
    }
}
