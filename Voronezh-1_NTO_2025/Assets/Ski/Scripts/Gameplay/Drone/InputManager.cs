using System;
using System.Collections;
using R3;
using UnityEngine;
using Valve.VR;

namespace Naf
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        [SerializeField] private float _pitchInput = default;
        [SerializeField] private float _rollInput = default;
        [SerializeField] private float _yawInput = default;
        [SerializeField] private float _throttleInput = default;

        public float PitchInput { get { return _pitchInput; } }//высота/наклон
        public float RollInput { get { return _rollInput; } }//крен
        public float YawInput { get { return _yawInput; } }//рысканье
        public float ThrottleInput { get { return _throttleInput; } }//сила


        //private SteamVR_Action_Vector2 _leftStickAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("LeftJoyStick");
        //private SteamVR_Action_Vector2 _rightStickAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("RightJoyStick");
        
        /*public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
        
        public SteamVR_Action_Boolean snapLeftAction = SteamVR_Input.GetBooleanAction("SnapTurnLeft");
        public SteamVR_Action_Boolean snapRightAction = SteamVR_Input.GetBooleanAction("SnapTurnRight");*/
        
        //public SteamVR_Action_Boolean back = SteamVR_Input.GetBooleanAction("Back");


        /*private bool _isActive = false;
        private Subject<Unit> _returnSubject = new Subject<Unit>();
        
        public Subject<Unit> SetActivete(bool active)
        {
            _isActive = active;
            return _returnSubject;
        }*/
        
        private void Awake()
        {
            //SetActivete(false);
           
            if (InputManager.Instance == null)
            {
                Instance = this;
            }
            else if (InputManager.Instance != this)
            {
                Destroy(gameObject);
            }
        }

        /*private void OnEnable()
        {
            back.onChange += Back;
        }

        private void OnDisable()
        {
            back.onChange -= Back;
        }*/

        /*private void Back(SteamVR_Action_Boolean fromaction, SteamVR_Input_Sources fromsource, bool newstate)
        {
            SetActivete(false);
            _returnSubject.OnNext(Unit.Default);
        }*/

        private void Update()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            float verticalS = Input.GetAxis("VerticalS");
            float horizontalS = Input.GetAxis("HorizontalS");
            
            
            float sense = 0.2f;
                //print(_leftStickAction.axis.y);
            OnRollInputChanged(horizontal * sense); 
            OnPitchInputChanged(vertical * sense);
                
            OnYawInputChanged(horizontalS * sense * 2.5f);
            OnThrottleInputChanged(verticalS * sense);
            
        }

        public bool IsInputIdle()
        {
            return Mathf.Approximately(_pitchInput, 0f) && Mathf.Approximately(_rollInput, 0f) && Mathf.Approximately(_throttleInput, 0f);
        }

        private void SetInputValue(ref float axis, float value)
        {
            axis = value;
        }

        private void OnPitchInputChanged(float value)
        {
            SetInputValue(ref _pitchInput, value);
        }

        private void OnRollInputChanged(float value)
        {
            SetInputValue(ref _rollInput, value);
        }

        private void OnYawInputChanged(float value)
        {
            SetInputValue(ref _yawInput, value);
        }

        private void OnThrottleInputChanged(float value)
        {
            SetInputValue(ref _throttleInput, value);
        }
    }
}
