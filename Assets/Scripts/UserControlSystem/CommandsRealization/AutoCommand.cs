using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;

public class AutoCommand
{
    public bool AutoMoveUnit(ISelectable unit, Vector3 target)
    {
        if (unit == null)
            return false;

        var moveExecutor = unit.PivotPoint.GetComponentInChildren<CommandExecutorBase<IMoveCommand>>();
        if (moveExecutor == null)
            return false;

        moveExecutor.ExecuteSpecificCommand(new MoveCommand(target));
        Debug.Log("AutoMoveUnit");
        return true;
    }
}
