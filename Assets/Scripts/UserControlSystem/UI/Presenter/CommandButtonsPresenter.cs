using System;
using System.Collections.Generic;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.View;
using Utils;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public sealed class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private CommandButtonsView _view;
        [Inject] private IObservable<ISelectable> _selectedValues;
        [Inject] private CommandButtonsModel _model;
        private ISelectable _currentSelectable;
        
        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectedValues.Subscribe(ONSelected);
        }

        private void ONSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }
            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
            }
            _currentSelectable = selectable;

            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                var queue = (selectable as Component).GetComponentInParent<ICommandsQueue>();
                _view.MakeLayout(commandExecutors, queue);
            }
        }
    }
}