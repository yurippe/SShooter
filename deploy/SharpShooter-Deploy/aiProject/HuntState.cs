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
            if (vector.DamageProb > 0)
            {
                return new AimForEnemyState(this).tick(ref action, vector, controller);
            }

            //TODO: If at a wall then rotate randomly
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

            //TODO: Do all wavering searching

            //TODO: If about changing to sidestepping
            //Are currently seeing the enemy, overwrite all of this with said state

            //Keep hunting him down
            return new PrepState(this);
        }
    }
}