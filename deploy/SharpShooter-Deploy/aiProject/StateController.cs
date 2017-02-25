using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class StateController
    {

        private State currentState = new InitialState();
        private FeatureVector previousFeatureVector;

        private int isPrepTick = 0;
        private bool prepping = false;

        public void tick(ref PlayerAction action, FeatureVector vector)
        {
            State oState = overrideState(vector);
            if (oState != null)
            {
                currentState = oState;
            }

            currentState = currentState.tick(ref action, vector, this);
            if (isPrepTick > 0) {
                isPrepTick -= 1;
            }

            previousFeatureVector = vector;

            if (action == PlayerAction.Prepare) {
                prepping = true;
            }
            if(prepping && isPrepTick == 0) {
                isPrepTick = 2;
                prepping = false;
            }
        }


        public bool isPrepMove()
        {
            return 2 == isPrepTick;
        }

        public bool hasPrepped()
        {
            return prepping;
        }

        public FeatureVector getPreviousFeatureVector()
        {
            return this.previousFeatureVector;
        }

        public bool wasShotLastRound(FeatureVector vector)
        {
            return previousFeatureVector.Health > vector.Health;
        }

        private State overrideState(FeatureVector vector)
        {

            if (vector.TickCount == 3200)
            {
                return new InitialState();
            }

            if (vector.DamageProb >= 0.8f && vector.ShootDelay == 0)
            {
                return new ShootDamnItState(new PrepState(new HuntState()));
            }

            if (vector.TickCount > 2 && wasShotLastRound(vector) && vector.ShootDelay < 5)
            {
                return new PrepState(new LoopAimUntillFindState());
            }

            return null;
        }
    }
}
