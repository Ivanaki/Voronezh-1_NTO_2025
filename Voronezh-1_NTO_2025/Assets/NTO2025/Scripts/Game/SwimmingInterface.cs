using System;
using System.Collections;
using UnityEngine;

namespace NTO2025.Scripts.Game
{
    public class SwimmingInterface : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _lerpSpeed = 1.5f;
        
        
        [SerializeField] private float _rotAngle = 20f;
        [SerializeField] private float _returnAngle = 3f;

        private void Start()
        {
            StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
            while (true)
            {
                float angleDifference = Math.Abs(transform.rotation.eulerAngles.y -
                                                 _camera.transform.rotation.eulerAngles.y);

                if (angleDifference > _rotAngle)
                {
                    while (angleDifference > _returnAngle)
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, _camera.transform.rotation, _lerpSpeed * Time.deltaTime);
                        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                        
                        angleDifference = Math.Abs(transform.rotation.eulerAngles.y -
                                                   _camera.transform.rotation.eulerAngles.y);;
                        
                        yield return null;
                    }
                }

                yield return null;
            }
        }
    }
}