using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing
{
    interface State
    {

        State tick(ref Torque3D.Engine.Util.Enums.PlayerAction action, Torque3D.Util.FeatureVector vector);

    }
}
