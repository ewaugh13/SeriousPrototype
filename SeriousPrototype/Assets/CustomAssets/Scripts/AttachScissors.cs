using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.Extras
{
    public class AttachScissors : MonoBehaviour
    {
        #region Instance Variables
        [Tooltip("The prefab to spawn")]
        [SerializeField]
        private GameObject prefab = null;
        [Tooltip("The rigid body to attach to")]
        [SerializeField]
        private Rigidbody attachPoint = null;
        [Tooltip("The action to spawn the scissors")]
        [SerializeField]
        private SteamVR_Action_Boolean spawn = null;

        [Tooltip("The object to track for actions")]
        [SerializeField]
        private SteamVR_Behaviour_Pose trackedObj = null;

        [Tooltip("The time to wait till destorying the spawned object (should be time it takes for scissors to cut)")]
        [SerializeField]
        private float timeToWaitTillDestroy = 0.2f;
        #endregion

        #region Hidden Variables
        private FixedJoint joint;
        private GameObject scissors;

        private GameObject scissorsTop;
        private GameObject scissorsBot;

        private GameObject refScissorsTop;
        private GameObject refScissorsBot;

        private const string SCISSOR_MODEL = "ScissorModel";
        private const string SCISSORS_TOP = "Scissors_Top";
        private const string SCISSORS_BOTTOM = "Scissors_Bottom";

        private float timerForDestruction = 0.0f;
        #endregion

        private void FixedUpdate()
        {
            // instantiate scissors
            if (joint == null && spawn.GetStateDown(trackedObj.inputSource))
            {
                scissors = GameObject.Instantiate(prefab);
                scissors.transform.position = attachPoint.transform.position;
                scissors.transform.rotation = attachPoint.transform.rotation;

                joint = scissors.AddComponent<FixedJoint>();
                joint.connectedBody = attachPoint;

                scissorsTop = scissors.transform.Find(SCISSORS_TOP).gameObject;
                scissorsBot = scissors.transform.Find(SCISSORS_BOTTOM).gameObject;

                refScissorsTop = attachPoint.transform.Find(SCISSOR_MODEL).transform.Find(SCISSORS_TOP).gameObject;
                refScissorsBot = attachPoint.transform.Find(SCISSOR_MODEL).transform.Find(SCISSORS_BOTTOM).gameObject;
            }
            // reset timer for deletion
            else if(spawn.GetStateDown(trackedObj.inputSource))
            {
                timerForDestruction = 0.0f;
            }
            // start timer for delete scissors
            else if (spawn.GetStateUp(trackedObj.inputSource))
            {
                timerForDestruction += Time.deltaTime;
            }
            // update scissors pieces
            else if (scissors != null)
            {
                scissorsTop.transform.position = new Vector3(refScissorsTop.transform.position.x, refScissorsTop.transform.position.y, refScissorsTop.transform.position.z);
                scissorsTop.transform.rotation = new Quaternion(refScissorsTop.transform.rotation.x, refScissorsTop.transform.rotation.y,
                    refScissorsTop.transform.rotation.z, refScissorsTop.transform.rotation.w);

                scissorsBot.transform.position = new Vector3(refScissorsBot.transform.position.x, refScissorsBot.transform.position.y, refScissorsBot.transform.position.z);
                scissorsBot.transform.rotation = new Quaternion(refScissorsBot.transform.rotation.x, refScissorsBot.transform.rotation.y,
                    refScissorsBot.transform.rotation.z, refScissorsBot.transform.rotation.w);

                // if the timer started increment until it needs to be deleted
                if(timerForDestruction > 0)
                {
                    timerForDestruction += Time.deltaTime;
                    if (timerForDestruction > timeToWaitTillDestroy)
                    {
                        // destroy objects and reset timer
                        if (joint != null)
                        {
                            Object.Destroy(joint);
                        }
                        if (scissors != null)
                        {
                            Object.Destroy(scissors);
                        }
                        timerForDestruction = 0.0f;
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.tag.Equals("Coral"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
