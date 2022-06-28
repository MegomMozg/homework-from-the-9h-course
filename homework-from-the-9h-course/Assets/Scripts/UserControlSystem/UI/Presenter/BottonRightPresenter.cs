using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace UserControlSystem
{
    public class BottonRightPresenter : MonoBehaviour
    {
        [SerializeField] private Button _Move;
        [SerializeField] private Button _Attach;
        [SerializeField] private Button _Patrol;
        [SerializeField] private Button _HoldPosition;
        
        private void Awake()
        {
            _Move.onClick.AddListener(TextExpansionMove);
            _Attach.onClick.AddListener(TextExpansionAttach);
            _Patrol.onClick.AddListener(TextExpansionPatrol);
            _HoldPosition.onClick.AddListener(TextExpansionHoldPosition);
        }

        private void OnDestroy()
        {
            _Move.onClick.RemoveAllListeners();
            _Attach.onClick.RemoveAllListeners();
            _Patrol.onClick.RemoveAllListeners();
            _HoldPosition.onClick.RemoveAllListeners();
        }

        private void TextExpansionMove()
        {
            Debug.LogWarning("Move");
        }
        private void TextExpansionAttach()
        {
            Debug.LogWarning("Attach");
        }
        private void TextExpansionPatrol()
        {
            Debug.LogWarning("Patrol");
        }
        private void TextExpansionHoldPosition()
        {
            Debug.LogWarning("HoldPosition");
        }
    }
}