using UnityEngine;
using Valve.VR.InteractionSystem;

namespace NTO2025.Scripts
{
    public class DroneTumbler: MonoBehaviour
    {
        [SerializeField] private GameObject _droneCamera;
         
        private bool _isDrone = false;

        private void Start()
        {
            Player.instance.hmdTransforms[0].GetComponent<Camera>().enabled = !_isDrone;
            _droneCamera.SetActive(_isDrone);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _isDrone = !_isDrone;
                Player.instance.hmdTransforms[0].GetComponent<Camera>().enabled = !_isDrone;
                _droneCamera.SetActive(_isDrone);
            }
        }
    }
}