using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
    class LoopAimUntillFindState : AimForEnemyState
    {
        private int startTick;
        public LoopAimUntillFindState(int startTick) : base(null)
        {
            this.startTick = startTick;
        }

        override protected State getNextState(FeatureVector vector)
        {
            if(vector.TickCount >= startTick + 150)
            {
                return new HuntState();
            }
            return this;
        }
    }
}
