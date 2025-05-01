using TripleA.StateMachine.FSM;

namespace Smash.Player.States
{
    public class PlayerBaseState : BaseState
    {
        protected readonly CharacterPawn _pawn;
        
        protected PlayerBaseState(CharacterPawn pawn)
        {
            _pawn = pawn;
        }
    }

    public class PlayerInit : BaseState
    {
    }
}
