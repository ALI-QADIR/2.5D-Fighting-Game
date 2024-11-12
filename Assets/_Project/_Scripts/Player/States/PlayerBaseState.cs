using TripleA.FSM;

namespace Smash.Player.States
{
    public class PlayerBaseState : BaseState
    {
        protected readonly PlayerController _controller;
        protected readonly PlayerAnimator _animator;
        
        protected PlayerBaseState(PlayerController controller, PlayerAnimator animator)
        {
            _controller = controller;
            _animator = animator;
        }
    }

    public class PlayerInit : BaseState
    {
    }
}
