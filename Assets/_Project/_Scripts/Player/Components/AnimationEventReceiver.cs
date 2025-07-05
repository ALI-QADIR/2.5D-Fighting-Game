using System;
using Smash.StructsAndEnums;
using TripleA.Utils.Extensions;
using UnityEngine;

namespace Smash.Player.Components
{
	public class AnimationEventReceiver : TripleA.AnimationEvents.AnimationEventReceiver
	{
		[SerializeField] private CharacterPawn m_pawn;

		private void Awake()
		{
			m_pawn ??= gameObject.GetOrAddComponent<CharacterPawn>();
		}

		public void BeginAttack(string attackType)
		{
			switch (attackType)
			{
				case "MainAttack":
					m_pawn.mainAttackStrategy.CanAttack = true;
					break;
				case "SideMainAttack":
					m_pawn.sideMainAttackStrategy.CanAttack = true;
					break;
				case "UpMainAttack":
					m_pawn.upMainAttackStrategy.CanAttack = true;
					break;
				case "DownMainAttack":
					m_pawn.downMainAttackStrategy.CanAttack = true;
					break;
				case "SpecialAttack":
					m_pawn.specialAttackStrategy.CanAttack = true;
					break;
				case "SideSpecialAttack":
					m_pawn.sideSpecialAttackStrategy.CanAttack = true;
					break;
				case "UpSpecialAttack":
					m_pawn.upSpecialAttackStrategy.CanAttack = true;
					break;
				case "DownSpecialAttack":
					m_pawn.downSpecialAttackStrategy.CanAttack = true;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(attackType), attackType, "Invalid attack type string");
			}
		}

		public void EndAttack(string attackType)
		{
			switch (attackType)
			{
				case "MainAttack":
					m_pawn.mainAttackStrategy.CanAttack = false;
					break;
				case "SideMainAttack":
					m_pawn.sideMainAttackStrategy.CanAttack = false;
					break;
				case "UpMainAttack":
					m_pawn.upMainAttackStrategy.CanAttack = false;
					break;
				case "DownMainAttack":
					m_pawn.downMainAttackStrategy.CanAttack = false;
					break;
				case "SpecialAttack":
					m_pawn.specialAttackStrategy.CanAttack = false;
					break;
				case "SideSpecialAttack":
					m_pawn.sideSpecialAttackStrategy.CanAttack = false;
					break;
				case "UpSpecialAttack":
					m_pawn.upSpecialAttackStrategy.CanAttack = false;
					break;
				case "DownSpecialAttack":
					m_pawn.downSpecialAttackStrategy.CanAttack = false;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(attackType), attackType, "Invalid attack type string");
			}
		}

		public void ExecuteOneHitAttack(string attackType)
		{
			switch (attackType)
			{
				case "MainAttack":
					m_pawn.mainAttackStrategy.Attack();
					break;
				case "SideMainAttack":
					m_pawn.sideMainAttackStrategy.Attack();
					break;
				case "UpMainAttack":
					m_pawn.upMainAttackStrategy.Attack();
					break;
				case "DownMainAttack":
					m_pawn.downMainAttackStrategy.Attack();
					break;
				case "SpecialAttack":
					m_pawn.specialAttackStrategy.Attack();
					break;
				case "SideSpecialAttack":
					m_pawn.sideSpecialAttackStrategy.Attack();
					break;
				case "UpSpecialAttack":
					m_pawn.upSpecialAttackStrategy.Attack();
					break;
				case "DownSpecialAttack":
					m_pawn.downSpecialAttackStrategy.Attack();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(attackType), attackType, "Invalid attack type string");
			}
		}
	}
}
