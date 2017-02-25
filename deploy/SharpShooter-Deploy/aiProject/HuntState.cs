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
        private int move = 0;
        private int rotations = 0;
        private int rotationCount = 0;
        private TurnDirection dir = TurnDirection.LEFT;

        public enum TurnDirection { RIGHT, LEFT }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            //We see the enemy, let's engage
            if (vector.DamageProb > 0)
            {
                rotationCount = 0;
                //Close to shooting
                if(vector.ShootDelay <3)
                    return new AimForEnemyState(this).tick(ref action, vector, controller);

                //While reloading, let's get closer
                //TODO: Make a more fancy "getting" close state
                return new HarlemShakeState(new PrepState(this), vector, 7).tick(ref action, vector, controller);
            }

            if (vector.TicksSinceObservedEnemy < 5)
            {
                rotationCount = 0;
                return new HarlemShakeState(new PrepState(this), vector, 40).tick(ref action, vector, controller);
            }

            //If at a wall then rotate
            float leftDistance = vector.DistanceToObstacleLeft;
            float rightDistance = vector.DistanceToObstacleRight;
            const int CRITICAL_DISTANCE = 5;

            if (leftDistance < CRITICAL_DISTANCE || rightDistance < CRITICAL_DISTANCE)
            {
                float angle = 20F;
                if (rotationCount++ > 2)
                {
                    angle = 180F;
                }
                else if (leftDistance < CRITICAL_DISTANCE && rightDistance < CRITICAL_DISTANCE)
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
            rotationCount = 0;

            //Should we move or rotate this time?
            if (move > 0)
            {
                action = PlayerAction.MoveForward;
                move--;
                rotations = 20;
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
                    action = PlayerAction.MoveForward;
                    dir = dir == TurnDirection.LEFT ? TurnDirection.RIGHT : TurnDirection.LEFT;
                    move = 30;
                }
            }

            //Keep hunting him down
            return new PrepState(this);
        }
    }
}