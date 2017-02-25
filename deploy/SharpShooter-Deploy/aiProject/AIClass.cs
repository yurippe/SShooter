using System;
using Torque3D;
using Torque3D.Engine.Util.Enums;
using Torque3D.Util;

namespace GameAI
{
   public class AIClass
   {

      private State currentState = new InitialState();

      [ConsoleFunction]
      public static PlayerAction MyThinkMethod(FeatureVector vector)
      {
            PlayerAction pAction = PlayerAction.Prepare;
            currentState = currentState.tick(ref pAction, vector);
            return pAction;
      }
   }
}