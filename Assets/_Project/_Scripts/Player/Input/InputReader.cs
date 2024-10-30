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
            m_input.Player.Move.performed += OnMove;
            m_input.Player.Move.canceled += OnMove;
            m_input.Player.PrimaryAttack.performed += OnPrimaryAttack;
            m_input.Player.PrimaryAttack.performed += OnSecondaryAttack;
            m_input.Player.Jump.performed += OnJump;
            m_input.Player.TempDash.performed += OnDash;
            m_input.Player.Enable();
        }

        private void OnDisable()
        {
            m_input.Player.Move.performed -= OnMove;
            m_input.Player.Move.canceled -= OnMove;
            m_input.Player.PrimaryAttack.performed -= OnPrimaryAttack;
            m_input.Player.PrimaryAttack.performed -= OnSecondaryAttack;
            m_input.Player.Jump.performed -= OnJump;
            m_input.Player.TempDash.performed -= OnDash;
            m_input.Player.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            m_controller.Direction = context.ReadValue<Vector2>();
            // Debug.Log(context.ReadValue<Vector2>());
        }

        private void OnPrimaryAttack(InputAction.CallbackContext context)
        {
        }
        
        private void OnSecondaryAttack(InputAction.CallbackContext context)
        {
        }

        private void OnShield(InputAction.CallbackContext context)
        {
        }
        
        private void OnDash(InputAction.CallbackContext context)
        {
            m_controller.HandleDashInput();
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            m_controller.HandleJumpInput();
        }
    }
}
