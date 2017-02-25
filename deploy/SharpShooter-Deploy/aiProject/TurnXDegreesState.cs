using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class TurnXDegreesState : State
    {
        public enum TurnDirection { RIGHT, LEFT }

        private State nextState;
        private TurnDirection direction;
        private float targetRotation;
        private float rotationSoFar = 0.0f;

        public TurnXDegreesState(State nextState, TurnDirection direction, float degrees = 90)
        {
            this.nextState = nextState;
            this.direction = direction;
            this.targetRotation = (float) (degrees * Math.PI / 180);
        }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            rotationSoFar += Math.Abs(vector.DeltaRot);

            if (rotationSoFar >= targetRotation)
            {
                return nextState.tick(ref action, vector, controller);
            }


            if (direction == TurnDirection.RIGHT) {
                action = PlayerAction.TurnRight;
            } else
            {
                action = PlayerAction.TurnLeft;
            }
            return this;
        }
    }
}
