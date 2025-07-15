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
					m_pawn.mainAbilityStrategy.CanAttack = true;
					break;
				case "SideMainAttack":
					m_pawn.sideMainAbilityStrategy.CanAttack = true;
					break;
				case "UpMainAttack":
					m_pawn.upMainAbilityStrategy.CanAttack = true;
					break;
				case "DownMainAttack":
					m_pawn.downMainAbilityStrategy.CanAttack = true;
					break;
				case "SpecialAttack":
					m_pawn.specialAbilityStrategy.CanAttack = true;
					break;
				case "SideSpecialAttack":
					m_pawn.sideSpecialAbilityStrategy.CanAttack = true;
					break;
				case "UpSpecialAttack":
					m_pawn.upSpecialAbilityStrategy.CanAttack = true;
					break;
				case "DownSpecialAttack":
					m_pawn.downSpecialAbilityStrategy.CanAttack = true;
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
					m_pawn.mainAbilityStrategy.CanAttack = false;
					break;
				case "SideMainAttack":
					m_pawn.sideMainAbilityStrategy.CanAttack = false;
					break;
				case "UpMainAttack":
					m_pawn.upMainAbilityStrategy.CanAttack = false;
					break;
				case "DownMainAttack":
					m_pawn.downMainAbilityStrategy.CanAttack = false;
					break;
				case "SpecialAttack":
					m_pawn.specialAbilityStrategy.CanAttack = false;
					break;
				case "SideSpecialAttack":
					m_pawn.sideSpecialAbilityStrategy.CanAttack = false;
					break;
				case "UpSpecialAttack":
					m_pawn.upSpecialAbilityStrategy.CanAttack = false;
					break;
				case "DownSpecialAttack":
					m_pawn.downSpecialAbilityStrategy.CanAttack = false;
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
					m_pawn.mainAbilityStrategy.Attack();
					break;
				case "SideMainAttack":
					m_pawn.sideMainAbilityStrategy.Attack();
					break;
				case "UpMainAttack":
					m_pawn.upMainAbilityStrategy.Attack();
					break;
				case "DownMainAttack":
					m_pawn.downMainAbilityStrategy.Attack();
					break;
				case "SpecialAttack":
					m_pawn.specialAbilityStrategy.Attack();
					break;
				case "SideSpecialAttack":
					m_pawn.sideSpecialAbilityStrategy.Attack();
					break;
				case "UpSpecialAttack":
					m_pawn.upSpecialAbilityStrategy.Attack();
					break;
				case "DownSpecialAttack":
					m_pawn.downSpecialAbilityStrategy.Attack();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(attackType), attackType, "Invalid attack type string");
			}
		}
	}
}
