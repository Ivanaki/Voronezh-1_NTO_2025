using Ezereal;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace NTO2025.Scripts
{
    [RequireComponent(typeof(MyLinearDrive))]
    public class ReturnLinearDrive : MonoBehaviour
    {
        [SerializeField] private EzerealCarController _ezerealCarController;
        [SerializeField] private Transform[] _positions;

        private MyLinearDrive _linearDrive;

        private void Awake()
        {
            _linearDrive = GetComponent<MyLinearDrive>();
            _linearDrive._detech.AddListener(Detach);
        }
        
        private void Detach()
        {
            //string bestName = "";
            Transform best = _positions[0];
            foreach (Transform t in _positions)
            {
                if (Vector3.Distance(transform.position, t.position) <
                    Vector3.Distance(transform.position, best.position))
                {
                    best = t;
                }
            }
            
            _linearDrive.SetPosition(best);
            
            _ezerealCarController.SetGear(best.name);
        }
    }
}