using Smash.Player.Components;
using TripleA.StateMachine.FSM;

namespace Smash.Player.States
{
    public class PlayerBaseState : BaseState
    {
        protected readonly CharacterPawn _pawn;
        protected readonly PlayerGraphicsController _graphicsController;
        
        protected PlayerBaseState(CharacterPawn pawn, PlayerGraphicsController graphicsController)
        {
            _pawn = pawn;
            _graphicsController = graphicsController;
        }
    }

    public class PlayerInit : BaseState
    {
    }
}
