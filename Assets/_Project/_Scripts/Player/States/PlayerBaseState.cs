using TripleA.StateMachine.FSM;

namespace Smash.Player.States
{
    public class PlayerBaseState : BaseState
    {
        protected readonly PlayerPawn _pawn;
        
        protected PlayerBaseState(PlayerPawn pawn)
        {
            _pawn = pawn;
        }
    }

    public class PlayerInit : BaseState
    {
    }
}
