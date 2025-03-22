using UnityEngine;
namespace Smash.Player.Input
{
    public class PlayerInputActionsController : MonoBehaviour
    {
        public PlayerInputActions InputActions { get; private set; }

        public PlayerInputActions InitialiseInputActions()
        {
            InputActions = new PlayerInputActions();
            InputActions.Enable();
            InputActions.Player.Disable();
            InputActions.UI.Disable();
            return InputActions;
        }
		
        public void SetPlayerInputEnabled(bool enable)
        {
            if (enable)
                EnablePlayerInput();
            else
                DisablePlayerInput();
        }
        
        public void SetUiInputEnabled(bool enable)
        {
            if (enable)
                EnableUiInput();
            else
                DisableUiInput();
        }

        private void EnablePlayerInput() => InputActions.Player.Enable();

        private void DisablePlayerInput() => InputActions.Player.Disable();

        private void EnableUiInput() => InputActions.UI.Enable();

        private void DisableUiInput() => InputActions.UI.Disable();
    }
}
