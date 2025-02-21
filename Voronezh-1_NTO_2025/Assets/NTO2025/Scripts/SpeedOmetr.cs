using System.Security.Claims;
using Ezereal;
using UnityEngine;

namespace NTO2025.Scripts
{
    public class SpeedOmetr : MonoBehaviour
    {
        [SerializeField] private Transform _rotationStrelka;
        [SerializeField] private EzerealCarController _ezerealCarController;

        private void Update()
        {
            _rotationStrelka.localEulerAngles = new Vector3(0, 0, -( _ezerealCarController.Speed/100 * 180 - 90)); 
        }
    }
}