using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class PrepState : State
    {
        private State nextState;
        public PrepState(State nextState)
        {
            this.nextState = nextState;
        }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            action = PlayerAction.Prepare;
            return nextState;
        }
    }
}
