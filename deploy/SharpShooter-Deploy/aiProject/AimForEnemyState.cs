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
                } else
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
                    return new AimForEnemyState(nextState, AimForEnemyDirection.RIGHT);
                } else
                {
                    action = PlayerAction.TurnLeft;
                    return new AimForEnemyState(nextState, AimForEnemyDirection.LEFT);
                }
            } else if(vector.DeltaDamageProb > 0)
            {
                if (direction == AimForEnemyDirection.LEFT)
                {
                    action = PlayerAction.TurnLeft;
                    return new AimForEnemyState(nextState, AimForEnemyDirection.LEFT);
                }
                else
                {
                    action = PlayerAction.TurnRight;
                    return new AimForEnemyState(nextState, AimForEnemyDirection.RIGHT);
                }
            } else
            {
              action = PlayerAction.Prepare;
              return nextState;
            }
        }
    }
}
