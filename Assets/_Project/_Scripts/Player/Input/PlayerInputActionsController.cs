using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Player.Input
{
    public class PlayerInputActionsController : MonoBehaviour
    {
        private PlayerInput PlayerInput { get; set; }

        #region Player Input Events

        public event Action<InputAction.CallbackContext> Horizontal;
        public event Action<InputAction.CallbackContext> Vertical;
        public event Action<InputAction.CallbackContext> SouthPerformed;
        public event Action<InputAction.CallbackContext> SouthCanceled;
        public event Action<InputAction.CallbackContext> EastPerformed;
        public event Action<InputAction.CallbackContext> EastCanceled;
        public event Action<InputAction.CallbackContext> NorthPerformed;
        public event Action<InputAction.CallbackContext> WestPerformed;
        public event Action<InputAction.CallbackContext> TriggerPerformed;
        public event Action<InputAction.CallbackContext> PausePerformed;

        #endregion Player Input Events

        #region Ui Input Events

        public event Action<InputAction.CallbackContext> NavigatePerformed;
        public event Action<InputAction.CallbackContext> CancelInputPerformed;
        public event Action<InputAction.CallbackContext> SubmitInputPerformed;
        public event Action<InputAction.CallbackContext> HorizontalScrollPerformed;
        public event Action<InputAction.CallbackContext> VerticalScrollPerformed;
        public event Action<InputAction.CallbackContext> ShoulderButtonPerformed;
        public event Action<InputAction.CallbackContext> ShoulderTriggerPerformed;
        public event Action<InputAction.CallbackContext> ResumePerformed;

        #endregion
        
        public void InitialiseInputActions(PlayerInput playerInput)
        {
            PlayerInput = playerInput;
            // PlayerInput.SwitchCurrentActionMap("Disabled");
        }

        public void SetPlayerInputEnabled()
        {
            PlayerInput.SwitchCurrentActionMap("Player");
        }
        
        public void SetUiInputEnabled()
        {
            PlayerInput.SwitchCurrentActionMap("UI");
        }

        public void DisableAllInput()
        {
            PlayerInput.SwitchCurrentActionMap("Disabled");
        }

        #region Event Invocations

        #region Player Input

        public void OnHorizontal(InputAction.CallbackContext context)
        {
            // TODO: Why does gamepad input first give 0 then other values?
            Horizontal?.Invoke(context);
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            // TODO: Why does gamepad input first give 0 then other values?
            Vertical?.Invoke(context);
        }

        public void OnNorth(InputAction.CallbackContext context)
        {
            if (context.started) NorthPerformed?.Invoke(context);
        }

        public void OnSouth(InputAction.CallbackContext context)
        {
            if (context.performed) SouthPerformed?.Invoke(context); 
            else if (context.canceled) SouthCanceled?.Invoke(context);
        }

        public void OnEast(InputAction.CallbackContext context)
        {
            if (context.performed) EastPerformed?.Invoke(context); 
            else if (context.canceled) EastCanceled?.Invoke(context);
        }

        public void OnWest(InputAction.CallbackContext context)
        {
            if (context.started) WestPerformed?.Invoke(context);
        }

        public void OnTrigger(InputAction.CallbackContext context)
        {
            if (context.started) TriggerPerformed?.Invoke(context);
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.started) PausePerformed?.Invoke(context);
        }

        #endregion Player Input

        #region Ui Input

        public void OnNavigate(InputAction.CallbackContext context)
        {
            if (context.started) NavigatePerformed?.Invoke(context);
        }
        
        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.started) CancelInputPerformed?.Invoke(context);
        }
        
        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.started) SubmitInputPerformed?.Invoke(context);
        }
        
        public void OnHorizontalScroll(InputAction.CallbackContext context)
        {
            HorizontalScrollPerformed?.Invoke(context);
        }
        
        public void OnVerticalScroll(InputAction.CallbackContext context)
        {
            VerticalScrollPerformed?.Invoke(context);
        }
        
        public void OnShoulderButton(InputAction.CallbackContext context)
        {
            if (context.started) ShoulderButtonPerformed?.Invoke(context);
        }
        
        public void OnShoulderTrigger(InputAction.CallbackContext context)
        {
            if (context.started) ShoulderTriggerPerformed?.Invoke(context);
        }
        
        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.canceled) ResumePerformed?.Invoke(context);
        }

        #endregion Ui Input

        #endregion Event Invocations
    }
}
