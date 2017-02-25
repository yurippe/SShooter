using System;
using Torque3D;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace Turing
{
   public class AIClass
   {

      static private StateController stateController = new StateController();

      [ConsoleFunction]
      public static PlayerAction Turing(FeatureVector vector)
      {
            PlayerAction pAction = PlayerAction.Prepare;
            stateController.tick(ref pAction, vector);
            return pAction;
      }
   }
}