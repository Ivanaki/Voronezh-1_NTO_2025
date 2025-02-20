using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Speedometer : MonoBehaviour
{
    [SerializeField]
    private float angle = 270;
    void Start()
    {
        
    }
    void Update()
    {
        this.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }
}