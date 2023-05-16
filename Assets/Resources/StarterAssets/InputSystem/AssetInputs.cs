using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
#endif

namespace StarterAssets
{
	public class AssetInputs : MonoBehaviour
	{
		[field: Header("Character Input Values")]
		[field: SerializeField]
		public Vector2 Move { get; private set; }
		[field: SerializeField]
		public Vector2 Look { get; private set; }
		[field: SerializeField]
		public bool Jump { get; set; }
		[field: SerializeField]
		public bool Sprint { get; private set; }

		[field: Header("Movement Settings")]
		[field: SerializeField]
		public bool analogMovement;

		[field: Header("Mouse Cursor Settings")]
		[field: SerializeField]
		public bool CursorLocked { get; private set; } = true;
		public bool CursorInputForLook { get; private set; } = true;
		private bool IsCharacterMovementActive { get; set; } = true;

		public void HandleMovementActionPerformed (CallbackContext callbackContext)
		{
			if (IsCharacterMovementActive == true)
			{
				MoveInput(callbackContext.ReadValue<Vector2>());
			}
		}
		public void HandleLookActionPerformed (CallbackContext callbackContext)
		{
			if (IsCharacterMovementActive == true)
			{
				LookInput(callbackContext.ReadValue<Vector2>());
			}
		}
		public void HandleJumpActionPerformed (CallbackContext callbackContext)
		{
			if (IsCharacterMovementActive == true)
			{
				JumpInput(callbackContext.ReadValueAsButton());
			}
		}
		public void HandleSprintActionPerformed (CallbackContext callbackContext)
		{
            if (IsCharacterMovementActive == true)
			{
				SprintInput(callbackContext.ReadValueAsButton());
			}
		}

		public void SetCharacterMovementActive (bool isEnabled)
		{
			IsCharacterMovementActive = isEnabled;
			SetCursorState(IsCharacterMovementActive);
		}

		public void OnLook(InputValue value)
		{
			if(CursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			Move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			Look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			Jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			Sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(CursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}