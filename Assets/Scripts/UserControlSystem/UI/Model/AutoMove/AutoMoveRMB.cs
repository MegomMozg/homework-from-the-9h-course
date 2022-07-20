using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using Core;
using Core.CommandExecutors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserControlSystem;
using Utils;
using Zenject;

public class AutoMoveRMB : MonoBehaviour
{
    [Inject] private AutoMoveRMBData _autoMoveRMBData;
    [Inject] private CommandCreatorBase<IMoveCommand> _commandCreator;
    private MoveCommandExecutor _commandExecutor;
    private ISelectable _thisUnit;
    private ChomperCommandsQueue _chomperCommandsQueue;

    private void Awake()
    {
        Debug.Log(_autoMoveRMBData);
        Debug.Log(_commandCreator);
        _commandExecutor = gameObject.GetComponentInParent<MoveCommandExecutor>();
        Debug.Log(_commandExecutor);
        _thisUnit = gameObject.GetComponentInParent<ISelectable>();
        Debug.Log(_thisUnit);
        _chomperCommandsQueue = gameObject.GetComponentInParent<ChomperCommandsQueue>();
        Debug.Log(_chomperCommandsQueue);
    }

    private void Start()
    {
        _autoMoveRMBData.vector3Value.OnNewValue += RMB_Pressed;
    }

    private void RMB_Pressed(Vector3 vector3)
    {
        if (_autoMoveRMBData.selectableValue.CurrentValue != _thisUnit)
            return;
        _chomperCommandsQueue.EnqueueCommand(new MoveCommand(vector3));

    }
}
