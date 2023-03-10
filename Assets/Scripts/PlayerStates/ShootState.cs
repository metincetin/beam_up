using BeamUp;
using BeamUp.FSM;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.PlayerStates
{
	public class ShootState : State
	{
		private Player _player;
		private Vector2 _shootDragStartPos;

		private const float REQUIRED_SHOOT_DRAG_DISTANCE = 2;

		private Vector2 PointerPosition => _player.GameInput.Gameplay.PointerPosition.ReadValue<Vector2>();

		public event Action Shot;

		private Coroutine _dragToBeginTextDisplayCoroutine;


		public ShootState(Player player)
		{
			this._player = player;
		}

		public override void Enter()
		{
			_player.GameInput.Gameplay.ShootDrag.started += OnShootDragStarted;
			_player.GameInput.Gameplay.ShootDrag.canceled += OnShootDragEnded;

			_player.FollowTarget.transform.position = Vector3.zero;

			_dragToBeginTextDisplayCoroutine = _player.StartCoroutine(ShowDragToBeginDelayed());
		}

		private IEnumerator ShowDragToBeginDelayed()
		{
			yield return new WaitForSeconds(1);
			_player.UIController.DragToBeginTutorialText.DOFade(1, 1f);
		}

		public override void Exit()
		{
			_player.GameInput.Gameplay.ShootDrag.started -= OnShootDragStarted;
			_player.GameInput.Gameplay.ShootDrag.canceled -= OnShootDragEnded;
			if (_dragToBeginTextDisplayCoroutine != null)
			{
				_player.StopCoroutine(_dragToBeginTextDisplayCoroutine);
			}
		}

		private void OnShootDragStarted(InputAction.CallbackContext obj)
		{
			_shootDragStartPos = PointerPosition;
			_player.ShootLineRenderer.enabled = true;
		}

		private void OnShootDragEnded(InputAction.CallbackContext obj)
		{
			var pointerPos = PointerPosition;
			var dist = Vector2.Distance(_shootDragStartPos, pointerPos);

			if (dist >= REQUIRED_SHOOT_DRAG_DISTANCE)
			{
				_player.LightbeamController.Spawn(Vector3.zero);
				Shoot(pointerPos - _shootDragStartPos);
				Shot?.Invoke();
			}
			_player.ShootLineRenderer.enabled = false;
		}

		public override void Update(float deltaTime)
		{
			var shootLine = _player.ShootLineRenderer;
			if (shootLine.enabled)
			{
				var offset = (PointerPosition - _shootDragStartPos);
				if (offset.magnitude > 1)
				{
					var dir = offset.normalized;
					_player.ShootLineRenderer.SetPosition(1, ((Vector3)dir) * REQUIRED_SHOOT_DRAG_DISTANCE);
				}
			}
		}

		public void Shoot(Vector3 direction)
		{
			_player.LightbeamController.Instances[0].Shoot(direction);
		}
	}
}
