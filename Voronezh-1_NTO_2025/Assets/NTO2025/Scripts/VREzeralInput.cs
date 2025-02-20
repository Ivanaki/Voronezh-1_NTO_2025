using System;
using Ezereal;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace NTO2025.Scripts
{
    [RequireComponent(typeof(EzerealCarController))]   
    public class VREzeralInput : MonoBehaviour
    {
        [SerializeField] private CircularDrive _circularDrive;
        [SerializeField] private CarInteraction _carInteraction;
        
        private SteamVR_Action_Single _leftTrigger = SteamVR_Input.GetAction<SteamVR_Action_Single>("LeftTrigger");
        private SteamVR_Action_Single _rightTrigger = SteamVR_Input.GetAction<SteamVR_Action_Single>("RightTrigger");
        private SteamVR_Action_Boolean _handBrake = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("HandBrake");

        private EzerealCarController _ezerealCarController;      
        
        
        private void Awake()
        {
            _ezerealCarController = GetComponent<EzerealCarController>();
        }

        private void Update()
        {
            if (_carInteraction.isPlayerInCar)
            {
                float handBrake = _handBrake.state ? 1 : 0;
                _ezerealCarController.OnBrake(_leftTrigger.axis);
                _ezerealCarController.OnAccelerate(_rightTrigger.axis);
                _ezerealCarController.OnHandbrake(handBrake);
            }
            _ezerealCarController.OnSteer(_circularDrive.linearMapping.value*2-1);
        }
    }
}