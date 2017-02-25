using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class HarlemShakeState : State
    {
        private int move = 0;
        private int rotations = 10;
        private TurnDirection dir;

        private int ticks = 0;
        private int timeRun;

        private State nextState;

        public enum TurnDirection { RIGHT, LEFT }

        public HarlemShakeState(State nextState, FeatureVector vector, int ticks)
        {
            this.nextState = nextState;
            timeRun = ticks;
            dir = vector.TickCount % 2 == 0 ? TurnDirection.RIGHT : TurnDirection.LEFT;
        }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            //We see the enemy, let's engage
            if (vector.DamageProb > 0 && vector.ShootDelay <3)
            {
                return new AimForEnemyState(this).tick(ref action, vector, controller);
            }

            if (timeRun < ticks++)
            {
                return nextState.tick(ref action, vector, controller);
            }

            if (rotations > 0)
            {
               if (move > 0)
               {
                   if(move == 1)
                       action = PlayerAction.MoveForward;
                   else
                       action = dir == TurnDirection.LEFT ? PlayerAction.MoveRight : PlayerAction.MoveLeft;
                   move--;
               }
               else
               {
                  action = dir == TurnDirection.LEFT ? PlayerAction.TurnLeft : PlayerAction.TurnRight;
                  move = 13;
               }
               rotations--;
           }
           else
           {
               dir = dir == TurnDirection.LEFT ? TurnDirection.RIGHT : TurnDirection.LEFT;
               rotations = 27;
           }

           //Keep looking
           ticks++;
           return new PrepState(this);
        }
    }
}