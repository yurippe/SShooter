using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class InitialState : State
    {
        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            action = PlayerAction.Prepare;

            return new TurnXDegreesState(new AimForEnemyState(new NOPState()), TurnXDegreesState.TurnDirection.RIGHT, 45);
        }
    }
}
