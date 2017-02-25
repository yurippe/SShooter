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
        private bool move = false;
        private TurnDirection dir = TurnDirection.LEFT;

        public enum TurnDirection { RIGHT, LEFT }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            //We see the enemy, let's engage
            if (vector.DamageProb > 0)
            {
                //Close to shooting
                if(vector.ShootDelay <3)
                    return new AimForEnemyState(new PrepState(this)).tick(ref action, vector, controller);

                //While reloading, let's get closer
                //TODO: Make a more fancy "getting" close state
                action = PlayerAction.MoveForward;
                return new PrepState(this);
            }

            //If at a wall then rotate
            float leftDistance = vector.DistanceToObstacleLeft;
            float rightDistance = vector.DistanceToObstacleRight;
            int CRITICAL_DISTANCE = 5;

            if (leftDistance < CRITICAL_DISTANCE || rightDistance < CRITICAL_DISTANCE)
            {
                float angle = 20F;
                if (leftDistance < CRITICAL_DISTANCE && rightDistance < CRITICAL_DISTANCE)
                {
                    angle = 70F;
                }
                return new PrepState(new TurnXDegreesState(this,
                                             leftDistance < rightDistance ?
                                                TurnXDegreesState.TurnDirection.RIGHT
                                              : TurnXDegreesState.TurnDirection.LEFT,
                                             angle
                    ).tick(ref action, vector, controller));
            }

            //Should we move or rotate this time?
            if (move)
            {
                action = PlayerAction.MoveForward;
                move = false;
            }
            else
            {
                if (dir == TurnDirection.LEFT)
                {
                    action = PlayerAction.TurnLeft;
                    dir = TurnDirection.RIGHT;
                }
                else
                {
                    action = PlayerAction.TurnRight;
                    dir = TurnDirection.LEFT;
                }
                move = false;
            }

            //Keep hunting him down
            return new PrepState(this);
        }
    }
}