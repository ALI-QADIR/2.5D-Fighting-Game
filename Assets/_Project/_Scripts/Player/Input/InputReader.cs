using TripleA.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smash.Player.Input
{
    [RequireComponent(typeof(PlayerController))]
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private PlayerController m_controller;
        private PlayerInputActions m_input;
        private void Awake()
        {
            m_input = new PlayerInputActions();
            m_controller ??= GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            m_input.Player.Horizontal.performed += OnHorizontal;
            m_input.Player.Horizontal.canceled += OnHorizontal;
            m_input.Player.Horizontal.started += OnLook;
            
            m_input.Player.Up.performed += OnUp;
            m_input.Player.Down.performed += OnDown;
            
            m_input.Player.MainAttack.performed += OnMainAttack;
            m_input.Player.SpecialAttack.performed += OnSpecialAttack;
            
            m_input.Player.Jump.performed += OnJump;
            m_input.Player.TempDash.performed += OnDash;
            
            m_input.Player.Shield.performed += OnShield;
            
            m_input.Player.Launch.performed += Launch;
            m_input.Player.LaunchAndCrash.performed += LaunchAndCrash;
            m_input.Player.LaunchAndFloat.performed += LaunchAndFloat;
            
            m_input.Player.Enable();
        }

        private void OnDisable()
        {
            m_input.Player.Horizontal.performed -= OnHorizontal;
            m_input.Player.Horizontal.canceled -= OnHorizontal;
            m_input.Player.Horizontal.started -= OnLook;
            
            m_input.Player.Up.performed -= OnUp;
            m_input.Player.Down.performed -= OnDown;
            
            m_input.Player.MainAttack.performed -= OnMainAttack;
            m_input.Player.SpecialAttack.performed -= OnSpecialAttack;
            
            m_input.Player.Jump.performed -= OnJump;
            m_input.Player.TempDash.performed -= OnDash;
            
            m_input.Player.Shield.performed -= OnShield;
            
            m_input.Player.Launch.performed -= Launch;
            m_input.Player.LaunchAndCrash.performed -= LaunchAndCrash;
            m_input.Player.LaunchAndFloat.performed -= LaunchAndFloat;
            
            m_input.Player.Disable();
        }

        private void OnHorizontal(InputAction.CallbackContext context)
        {
            m_controller.Direction = Vector3.zero.With(x: context.ReadValue<float>());
        }
        
        private void OnLook(InputAction.CallbackContext context)
        {
            m_controller.Rotate(context.ReadValue<float>());
        }

        private void OnUp(InputAction.CallbackContext context)
        {
            m_controller.HandleUpInput();
        }

        private void OnDown(InputAction.CallbackContext context)
        {
            m_controller.HandleDownInput();
        }

        private void OnMainAttack(InputAction.CallbackContext context)
        {
        }
        
        private void OnSpecialAttack(InputAction.CallbackContext context)
        {
        }

        private void OnShield(InputAction.CallbackContext context)
        {
        }
        
        private void OnDash(InputAction.CallbackContext context)
        {
            m_controller.HandleDashInput();
        }
        
        private void Launch(InputAction.CallbackContext context)
        {
            // m_controller.HandleLaunchInput();
        }
        
        private void LaunchAndCrash(InputAction.CallbackContext context)
        {
            // m_controller.HandleLaunchAndCrashInput();
        }
        
        private void LaunchAndFloat(InputAction.CallbackContext context)
        {
            // m_controller.HandleLaunchAndFloatInput();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            m_controller.HandleJumpInput();
        }
    }
}
