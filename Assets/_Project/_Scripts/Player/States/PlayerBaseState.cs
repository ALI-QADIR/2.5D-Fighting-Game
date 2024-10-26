using TripleA.FSM;

namespace Smash.Player.States
{
    public class PlayerBaseState : BaseState
    {
        protected PlayerController _controller;
        
        protected PlayerBaseState(PlayerController controller)
        {
            _controller = controller;
        }
    }

    public class PlayerInit : BaseState
    {

        public PlayerInit()
        {
        }
    }
}
