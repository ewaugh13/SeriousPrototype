using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem.Sample
{
    public class TurnOnBandSaw : MonoBehaviour
    {
        #region Instance Variables
        [Tooltip("The button to press")]
        [SerializeField]
        private HoverButton hoverButton = null;

        [Tooltip("The machine gameobject")]
        [SerializeField]
        private GameObject bandSawMachine = null;
        #endregion

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);
        }

        private void OnButtonDown(Hand hand)
        {
            turnOnMachine();
        }

        private void turnOnMachine()
        {
            BandSawBladeMovement bandSaw = bandSawMachine.GetComponentInChildren<BandSawBladeMovement>();
            bandSaw.setMachineOn(!bandSaw.getMachineOn());
        }
    }
}