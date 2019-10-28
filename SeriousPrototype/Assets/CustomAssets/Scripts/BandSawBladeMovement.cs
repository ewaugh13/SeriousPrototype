using System.Collections;
using UnityEngine;

public class BandSawBladeMovement : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("The start y position of the band saw blade")]
    [SerializeField]
    private float startYPosition = 0;
    [Tooltip("The end y position of the band saw blade")]
    [SerializeField]
    private float topYPostion = 0;
    [Tooltip("The smaller the time the faster the blade moves")]
    [SerializeField]
    private float timeToMove = .5f;
    [Tooltip("Whether the band saw machine is on or not")]
    [SerializeField]
    private bool machineOn;
    #endregion

    #region Hidden Variables
    private bool moving;

    private float xPosition;
    private float zPosition;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        moving = false;

        xPosition = this.gameObject.GetComponent<Transform>().localPosition.x;
        zPosition = this.gameObject.GetComponent<Transform>().localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (machineOn)
        {
            float localPosY = this.gameObject.GetComponent<Transform>().localPosition.y;
            if (!moving)
            {
                if (Mathf.Abs(localPosY - topYPostion) < 0.01)
                {
                    this.gameObject.GetComponent<Transform>().localPosition = new Vector3(xPosition, startYPosition, zPosition);
                }
                if (Mathf.Abs(localPosY - startYPosition) < 0.01)
                {
                    StartCoroutine(moveBlade(startYPosition, topYPostion, timeToMove));
                    moving = true;
                }
            }
        }
    }

    private IEnumerator moveBlade(float start, float end, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            float yValue = Mathf.Lerp(start, end, elapsedTime / time);
            this.gameObject.GetComponent<Transform>().localPosition = new Vector3(xPosition, yValue, zPosition);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= time)
            {
                this.moving = false;
            }
            yield return null;
        }
    }

    #region Getters and Setters
    public bool getMachineOn()
    {
        return this.machineOn;
    }

    public void setMachineOn(bool value)
    {
        this.machineOn = value;
    }
    #endregion
}
