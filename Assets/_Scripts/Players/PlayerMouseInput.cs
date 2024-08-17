using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    public class PlayerMouseInput : MonoBehaviour
    {
        [SerializeField] private ActionEvent m_onLeftMouseClickInWorld;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_onLeftMouseClickInWorld.Raise();
            }
        }
    }
}

