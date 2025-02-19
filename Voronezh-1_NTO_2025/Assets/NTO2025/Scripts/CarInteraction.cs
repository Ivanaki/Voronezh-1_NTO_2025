using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace NTO2025.Scripts
{
    public class CarInteraction : MonoBehaviour
    {
        private SteamVR_Action_Boolean _enterCar = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("EnterCar");
        
        public GameObject interactionUI; 
        public Transform playerInsidePosition;
        public Transform playerOutsidePosition;
        private bool isPlayerInRange = false;
        public bool isPlayerInCar = false;
        
        void Start()
        {
            interactionUI.SetActive(false);
        }

        void Update()
        {
            if ((isPlayerInRange || isPlayerInCar) && _enterCar.changed)
            {
                if (isPlayerInCar)
                {
                    ExitCar();
                }
                else
                {
                    EnterCar();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //print(other.gameObject.name);
            if (other.CompareTag("HeadCollider"))
            {
                isPlayerInRange = true;
                interactionUI.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("HeadCollider"))
            {
                isPlayerInRange = false;
                interactionUI.SetActive(false);
            }
        }

        private void EnterCar()
        {
            isPlayerInCar = true;
            
            // Здесь можно добавить код для отключения управления игроком
            var player = Player.instance;
            player.transform.SetParent(playerInsidePosition, false);
            player.transform.localPosition = new Vector3(0, 0, 0);
            player.transform.localRotation = Quaternion.identity;
        }

        private void ExitCar()
        {
            isPlayerInCar = false;
            
            // Здесь можно добавить код для включения управления игроком
            
            var player = Player.instance;
            player.transform.SetParent(null);
            player.transform.position = playerOutsidePosition.position;
            player.transform.rotation = playerOutsidePosition.rotation;
        }
    }
}