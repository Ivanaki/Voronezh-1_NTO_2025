using System;
using R3;
using UnityEngine;

namespace MySteamVR
{
    [RequireComponent(typeof(Collider))]
    public class PlayerTrigger : MonoBehaviour
    {
        private readonly Subject<Unit> _trigger = new Subject<Unit>();
        public Observable<Unit> Triggered => _trigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _trigger.OnNext(Unit.Default);
                gameObject.SetActive(false);
            }
        }
    }
}