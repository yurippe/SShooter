using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class AimForEnemyState : State
    {
        public enum AimForEnemyDirection { LEFT, RIGHT }
        private AimForEnemyDirection direction;
        private State nextState;

        public AimForEnemyState(State nextState, AimForEnemyDirection direction = AimForEnemyDirection.LEFT)
        {
            this.direction = direction;
            this.nextState = nextState;
        }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            if (controller.isPrepMove())
            {
                if (direction == AimForEnemyDirection.LEFT) {
                    action = PlayerAction.TurnLeft;
                }
                {
                    action = PlayerAction.TurnRight;
                }
                return this;
            }

            if(vector.DeltaDamageProb < 0)
            {
                if (direction == AimForEnemyDirection.LEFT)
                {
                    action = PlayerAction.TurnRight;
                }
                {
                    action = PlayerAction.TurnLeft;
                }
                return nextState;
            } else
            {
              action = PlayerAction.Prepare;
              return nextState;
            }
        }
    }
}
