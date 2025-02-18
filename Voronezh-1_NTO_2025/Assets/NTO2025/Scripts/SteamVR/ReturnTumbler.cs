using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTumbler : MonoBehaviour
{
    [SerializeField] private bool _returnInSpecificModes = false;
    [SerializeField] private int[] _modesToReturn;
    
    public void Return(int mode)
    {
        if (!_returnInSpecificModes)
        {
            SetBase();
        }
        else
        {
            foreach (var modeToReturn in _modesToReturn)
            {
                if (modeToReturn == mode)
                {
                    SetBase();
                }
            }
        }
    }
    
    private void SetBase()
    {
        if (TryGetComponent(out IStartSettings startSettings))
        {
            startSettings.SetStartSettings();
        }
    }
}
