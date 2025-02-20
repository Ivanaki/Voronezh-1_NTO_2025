using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    private bool isin = false;
    public void Update()
    {
        print(isin);
        if (global.checkpoints == -1)
        {
            isin = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RaceCar") && !isin && global.checkpoints != -1)
        {
            isin = true;
            global.checkpoints += 1;
        }
    }
}
