using System.Collections;
using UnityEngine;
using Valve.VR;

public class ScissorCut : MonoBehaviour
{
    #region Instance Variables
    [Tooltip("The action for squeezing scissors")]
    [SerializeField]
    private SteamVR_Action_Boolean scissorSqueeze = null;
    [Tooltip("The steam vr input source")]
    [SerializeField]
    private SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
    [Tooltip("The top half of the scissors")]
    [SerializeField]
    private GameObject scissorTop = null;
    [Tooltip("The bottom half of the scissors")]
    [SerializeField]
    private GameObject scissorBot = null;
    [Tooltip("The speed the scissors cut at")]
    [SerializeField]
    private float cutSpeed = 3;
    [Tooltip("The rotation amount (should be around 14)")]
    [SerializeField]
    private float rotateYAmount = 16;
    [Tooltip("The final x position amount")]
    [SerializeField]
    private float finalXPosition = 0.0025f;
    [Tooltip("The final z position amount")]
    [SerializeField]
    private float finalZPosition = 0.0055f;
    #endregion

    #region Hidden Variables
    private bool cutting = false;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        scissorSqueeze.AddOnStateDownListener(cutMotion, handType);
        scissorSqueeze.AddOnStateUpListener(cutMotion, handType);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void cutMotion(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (fromSource.Equals(SteamVR_Input_Sources.LeftHand))
        {
            if(this.gameObject.transform == null || this.gameObject.transform.parent == null || this.gameObject.transform.parent.gameObject == null)
            {
                return;
            }
            if (!this.gameObject.transform.parent.gameObject.name.Contains("LeftHand"))
            {
                return;
            }
        }
        else if (fromSource.Equals(SteamVR_Input_Sources.RightHand))
        {
            if (this.gameObject.transform == null || this.gameObject.transform.parent == null || this.gameObject.transform.parent.gameObject == null)
            {
                return;
            }
            if (!this.gameObject.transform.parent.gameObject.name.Contains("RightHand"))
            {
                return;
            }
        }
        float currentXPosTop = this.scissorTop.GetComponent<Transform>().localPosition.x;
        float currentYRotationTop = this.scissorTop.GetComponent<Transform>().localEulerAngles.y;
        float currentZPosTop = this.scissorTop.GetComponent<Transform>().localPosition.z;

        float currentXPosBot = this.scissorBot.GetComponent<Transform>().localPosition.x;
        float currentYRotationBot = this.scissorBot.GetComponent<Transform>().localEulerAngles.y;
        float currentZPosBot = this.scissorBot.GetComponent<Transform>().localPosition.z;

        // if the degrees need to be negative to for correct rotation
        if (currentYRotationBot > rotateYAmount)
        {
            currentYRotationBot -= 360;
        }
        if (currentYRotationTop > rotateYAmount)
        {
            currentYRotationTop -= 360;
        }

        if (!this.cutting)
        {
            // make the scissors look like they are cutting
            this.cutting = !this.cutting;
            StartCoroutine(moveScissor(scissorTop, currentYRotationTop, -rotateYAmount, currentXPosTop, -finalXPosition, currentZPosTop, -finalZPosition, cutSpeed, this.cutting));
            StartCoroutine(moveScissor(scissorBot, currentYRotationBot, rotateYAmount, currentXPosBot, finalXPosition, currentZPosBot, finalZPosition, cutSpeed, this.cutting));
        }
        else
        {
            // make the scissors look like they are retracting
            this.cutting = !this.cutting;
            StartCoroutine(moveScissor(scissorTop, currentYRotationTop, 0, currentXPosTop, 0, currentZPosTop, 0, cutSpeed, this.cutting));
            StartCoroutine(moveScissor(scissorBot, currentYRotationBot, 0, currentXPosBot, 0, currentZPosBot, 0, cutSpeed, this.cutting));
        }
    }

    private IEnumerator moveScissor(GameObject scissorHalf, float startYRotation, float endYRotation,
        float startXPos, float endXPos, float startZPos, float endZPos, float time, bool isCutting)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            if (isCutting != this.cutting)
            {
                elapsedTime = time;
                yield break;
            }
            float newXPos = Mathf.Lerp(startXPos, endXPos, elapsedTime / time);
            float newZPos = Mathf.Lerp(startZPos, endZPos, elapsedTime / time);
            scissorHalf.GetComponent<Transform>().localPosition = new Vector3(newXPos, 0, newZPos);

            float newYRotation = Mathf.Lerp(startYRotation, endYRotation, elapsedTime / time);
            scissorHalf.GetComponent<Transform>().localEulerAngles = new Vector3(0, newYRotation, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
