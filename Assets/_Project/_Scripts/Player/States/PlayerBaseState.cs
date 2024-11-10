using TripleA.FSM;

namespace Smash.Player.States
{
    public class PlayerBaseState : BaseState
    {
        protected readonly PlayerController _controller;
        
        protected PlayerBaseState(PlayerController controller)
        {
            _controller = controller;
        }
    }

    public class PlayerInit : BaseState
    {
    }
}
