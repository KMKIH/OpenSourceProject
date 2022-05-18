using UnityEngine;

namespace QuantumTek.QuantumUI
{
    /// <summary>
    /// QUI_Window is a basic UI element responsible for holding other UI elements.
    /// </summary>
    [AddComponentMenu("Quantum Tek/Quantum UI/Window")]
    [DisallowMultipleComponent]
    public class QUI_Window : QUI_Element
    {
        public override void SetActive(bool value)
        {
            bool oldActive = active;
            active = value;

            if (oldActive != active)
            {
                if (active)
                {
                    Time.timeScale = 0f;
                    onActive.Invoke();
                }
                else
                {
                    Time.timeScale = 1f;
                    onInactive.Invoke();
                }
            }
        }
    }
}