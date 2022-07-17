using System.Threading.Tasks;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutors
{
    public class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override async Task ExecuteSpecificCommand(ISetRallyPointCommand command)
        {
            GetComponent<MainBuilding>().rallyPoint = command.RallyPoint;
        }
    }
}