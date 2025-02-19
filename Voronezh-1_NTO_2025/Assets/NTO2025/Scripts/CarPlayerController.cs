using UnityEngine;
using Valve.VR;

namespace NTO2025.Scripts
{
    public class CarPlayerController:MonoBehaviour
    {
        [SerializeField] private float _speed = 1.5f;
        [SerializeField] private CarInteraction carInteraction;

        private SteamVR_Action_Vector2 _zx = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("PlayerCarZX");
        private SteamVR_Action_Vector2 _y = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("PlayerCarY");

        private void Update()
        {
            if (carInteraction.isPlayerInCar)
            {
                //print("inCar");
                Vector2 zxInput = _zx.axis;
                float yInput = _y.axis.y;
                
                MoveCar(zxInput, yInput);
            }
        }

        private void MoveCar(Vector2 zxInput, float yInput)
        {
            Vector3 moveDirection = new Vector3(zxInput.x, yInput, zxInput.y);
            carInteraction.playerInsidePosition.Translate(moveDirection * (Time.deltaTime * _speed)); 
        }
    }
}