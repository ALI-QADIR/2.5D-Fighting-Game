using TripleA.FSM;
using UnityEngine;

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
}
