using System;
using Utils;

namespace Core
{
    public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
    {
        private readonly UnitMovementStop _unitMovementStop;

        public StopAwaiter(UnitMovementStop unitMovementStop) : base()
        {
            _unitMovementStop = unitMovementStop;
            _unitMovementStop.OnStop += ONStop;
        }

        private void ONStop()
        {
            _unitMovementStop.OnStop -= ONStop;
            SetComleted();
        }

        public override AsyncExtensions.Void GetResult() => new AsyncExtensions.Void();
    }
}