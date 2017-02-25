using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class HuntState : State
    {
        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            //TODO: Do all wavering searching

            //TODO: If at a wall then rotate randomly

            //TODO: If about changing to sidestepping
            //Are currently seeing the enemy, overwrite all of this with said state

            //Keep hunting him down
            return this;
        }
    }
}