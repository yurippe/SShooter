using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class BreakoutState : State
        {
            const int CRITICAL_DISTANCE = 3;

            private int breakout = 0;
            private int timeRun;

            private State nextState;

            public BreakoutState(State nextState, FeatureVector vector, int ticks)
            {
                this.nextState = nextState;
                timeRun = ticks;
            }

            public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
            {
                float leftDistance = vector.DistanceToObstacleLeft;
                float rightDistance = vector.DistanceToObstacleRight

                if (leftDistance > CRITICAL_DISTANCE && rightDistance > CRITICAL_DISTANCE)
                {
                    action = PlayerAction.MoveForward;
                    if (breakout++ > timeRun)
                        return nextState;
                    else
                        return new PrepState(this);
                }
                else
                {
                    action = PlayerAction.TurnLeft;
                    return new PrepState(this);
                }
            }
        }
}