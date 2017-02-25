﻿using System;
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


        public void tick(ref PlayerAction action, FeatureVector vector)
        {
            currentState = currentState.tick(ref action, vector, this);
            previousFeatureVector = vector;
        }



        public bool wasShotLastRound(FeatureVector vector)
        {
            return previousFeatureVector.Health < vector.Health;
        }
    }
}
