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
            if (controller.isPrepMove())
            {
                action = PlayerAction.Prepare;
            }


            //TODO: If at a wall then rotate randomly
            float leftDistance = vector.DistanceToObstacleLeft;
            float rightDistance = vector.DistanceToObstacleRight;

            if (leftDistance < 5 && rightDistance < 5)
            {
                action =
            }
            else if (leftDistance < 5)
            {

            }
            else if (rightDistance < 5)
            {

            }

            //TODO: If about changing to sidestepping
            //Are currently seeing the enemy, overwrite all of this with said state

            //Keep hunting him down
            return this;
        }
    }
}