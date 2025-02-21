using System;
using Drift.Root;
using Game.Params;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace NTO2025.Scripts.Drift
{
    public class Exit : MonoBehaviour
    {
        public DriftEntryPoint DriftEntryPoint;
        
        private void OnTriggerEnter(Collider other)
        {
            print(other.name);
            if (other.CompareTag("Car"))
            {
                DontDestroyOnLoad(Player.instance.gameObject);
                DriftEntryPoint._exitSignalSub.OnNext(new GameplayExitParams(false));
            }
        }
    }
}