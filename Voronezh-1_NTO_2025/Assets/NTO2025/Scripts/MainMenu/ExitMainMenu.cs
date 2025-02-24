using System;
using UnityEngine;

namespace NTO2025.Scripts.MainMenu
{
    public class ExitMainMenu : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Paper"))
            {
                print("exirt");
                Application.Quit();
            }
        }
    }
}