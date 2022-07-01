using System;
using Abstractions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UserControlSystem;

namespace ObjectSelected
{
    public class ObjectSelectedPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedValue;

        private GameObject _currentSelect;
        private Outline _outline;

        void Start()
        {
            _selectedValue.OnSelected += OnSelected;
            OnSelected(_selectedValue.CurrentValue);
            
        }

        private void OnSelected(ISelectable selected)
        {
            SetOutline(selected);
        }

        private void SetOutline(ISelectable selected)
        {
            if (selected != null)
            {
                if (selected.GameObject.GetComponent<Outline>() == null)
                {
                    Outline outline = selected.GameObject.AddComponent<Outline>();
                    _outline = outline;
                    outline.OutlineColor = Color.red;
                    outline.OutlineWidth = 3;
                }
                else return;
            }
            else if(_outline!= null) Destroy(_outline.gameObject.GetComponent<Outline>());
        }
    }
}