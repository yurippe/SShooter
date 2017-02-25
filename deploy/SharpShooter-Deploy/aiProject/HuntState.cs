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
        private int move = 10;
        private int rotations = 0;
        private TurnDirection dir = TurnDirection.LEFT;

        public enum TurnDirection { RIGHT, LEFT }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            //We see the enemy, let's engage
            if (vector.DamageProb > 0)
            {
                //Close to shooting
                if(vector.ShootDelay <3)
                    return new AimForEnemyState(this).tick(ref action, vector, controller);

                //While reloading, let's get closer
                //TODO: Make a more fancy "getting" close state
                action = PlayerAction.MoveForward;
                return new PrepState(this);
            }

            if (vector.TicksSinceObservedEnemy < 5)
            {
                return new DodgeState(new PrepState(this), vector, 40);
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
                    angle = 90F;
                }
                return new PrepState(new TurnXDegreesState(this,
                                             leftDistance < rightDistance ?
                                                TurnXDegreesState.TurnDirection.RIGHT
                                              : TurnXDegreesState.TurnDirection.LEFT,
                                             angle
                    ).tick(ref action, vector, controller));
            }

            //Should we move or rotate this time?
            if (move > 0)
            {
                action = PlayerAction.MoveForward;
                move--;
                rotations = 8;
            }
            else
            {
                if (rotations > 0)
                {
                    action = dir == TurnDirection.LEFT ? PlayerAction.TurnLeft : PlayerAction.TurnRight;
                    rotations--;
                }
                else
                {
                    dir = dir == TurnDirection.LEFT ? TurnDirection.RIGHT : TurnDirection.LEFT;
                    move = 10;
                }
            }

            //Keep hunting him down
            return new PrepState(this);
        }
    }
}