using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class DodgeState : State
    {
        private bool move = false;
        private int rotations = 10;
        private TurnDirection dir;

        private int ticks;

        private State nextState;

        public enum TurnDirection { RIGHT, LEFT }

        public DodgeState(State nextState, FeatureVector vector, int ticks)
        {
            this.nextState = nextState;
            this.ticks = ticks;
            dir = vector.DeltaRot >= 0 ? TurnDirection.RIGHT : TurnDirection.LEFT;
        }

        public State tick(ref PlayerAction action, FeatureVector vector, StateController controller)
        {
            //We see the enemy, let's engage
            if (vector.DamageProb > 0 && vector.ShootDelay <3)
            {
                return new AimForEnemyState(this).tick(ref action, vector, controller);
            }

            if (vector.TicksSinceObservedEnemy > ticks)
            {
                return nextState;
            }

            if (rotations > 0)
            {
               if (move)
               {
                   action = dir == TurnDirection.LEFT ? PlayerAction.MoveRight : PlayerAction.MoveLeft;
                   move = false;
               }
               else
               {
                  action = dir == TurnDirection.LEFT ? PlayerAction.TurnLeft : PlayerAction.TurnRight;
                  move = true;
               }
               rotations--;
           }
           else
           {
               dir = dir == TurnDirection.LEFT ? TurnDirection.RIGHT : TurnDirection.LEFT;
               rotations = 10;
           }

           //Keep looking
           ticks++;
           return new PrepState(this);
        }
    }
}