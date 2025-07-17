using System;
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

		public void BeginAbilityScan(string abilityType)
		{
			switch (abilityType)
			{
				case "MainAttack":
					m_pawn.mainAbilityStrategy.CanScan = true;
					break;
				case "SideMainAttack":
					m_pawn.sideMainAbilityStrategy.CanScan = true;
					break;
				case "UpMainAttack":
					m_pawn.upMainAbilityStrategy.CanScan = true;
					break;
				case "DownMainAttack":
					// m_pawn.downMainAbilityStrategy.CanScan = true;
					break;
				case "SpecialAttack":
					m_pawn.specialAbilityStrategy.CanScan = true;
					break;
				case "SideSpecialAttack":
					// m_pawn.sideSpecialAbilityStrategy.CanScan = true;
					break;
				case "UpSpecialAttack":
					// m_pawn.upSpecialAbilityStrategy.CanScan = true;
					break;
				case "DownSpecialAttack":
					// m_pawn.downSpecialAbilityStrategy.CanScan = true;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, "Invalid attack type string");
			}
		}

		public void EndAbilityScan(string abilityType)
		{
			switch (abilityType)
			{
				case "MainAttack":
					m_pawn.mainAbilityStrategy.CanScan = false;
					break;
				case "SideMainAttack":
					m_pawn.sideMainAbilityStrategy.CanScan = false;
					break;
				case "UpMainAttack":
					m_pawn.upMainAbilityStrategy.CanScan = false;
					break;
				case "DownMainAttack":
					// m_pawn.downMainAbilityStrategy.CanScan = false;
					break;
				case "SpecialAttack":
					m_pawn.specialAbilityStrategy.CanScan = false;
					break;
				case "SideSpecialAttack":
					// m_pawn.sideSpecialAbilityStrategy.CanScan = false;
					break;
				case "UpSpecialAttack":
					// m_pawn.upSpecialAbilityStrategy.CanScan = false;
					break;
				case "DownSpecialAttack":
					// m_pawn.downSpecialAbilityStrategy.CanScan = false;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, "Invalid attack type string");
			}
		}

		public void ExecuteOneShotAbilityEffect(string abilityType)
		{
			switch (abilityType)
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
					// m_pawn.downMainAbilityStrategy.Attack();
					break;
				case "SpecialAttack":
					m_pawn.specialAbilityStrategy.Attack();
					break;
				case "SideSpecialAttack":
					// m_pawn.sideSpecialAbilityStrategy.Attack();
					break;
				case "UpSpecialAttack":
					// m_pawn.upSpecialAbilityStrategy.Attack();
					break;
				case "DownSpecialAttack":
					// m_pawn.downSpecialAbilityStrategy.Attack();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, "Invalid attack type string");
			}
		}
	}
}
