using Smash.Player.StructsAndEnums;
using UnityEngine;

namespace Smash.Player
{
	public class CapsuleCastSensor : ICastSensor
	{
		public float castDistance;
		public LayerMask layerMask = 255;

		private Vector3 m_bottom;
		private Vector3 m_top;
		private Vector3 m_worldBottom;
		private Vector3 m_worldTop;
		private Vector3 m_worldDirection;

		private float m_playerHeight;
		private float m_playerRadius;
		private const float k_safetyOffset = 0.001f;

		private readonly Transform m_tr;

		private Vector3 m_rayCastDirection;

		private RaycastHit m_hitInfo;

		public CapsuleCastSensor(Transform playerTR, CastDirection rayCastDirection, float playerHeight,
			float playerRadius)
		{
			m_tr = playerTR;
			m_rayCastDirection = GetCastDirection(rayCastDirection);
			m_playerHeight = playerHeight;
			m_playerRadius = playerRadius;

			m_bottom = Vector3.zero + m_tr.up * (m_playerRadius + k_safetyOffset);
			m_top = m_bottom + (m_playerHeight - (m_playerRadius + k_safetyOffset)) * m_tr.up;
		}

		public void Cast()
		{
			m_worldBottom = m_tr.TransformPoint(m_bottom);
			m_worldTop = m_tr.TransformPoint(m_top);
			m_worldDirection = GetCastDirection();
			
			Physics.CapsuleCast(
				point1: m_worldBottom,
				point2: m_worldTop,
				radius: m_playerRadius,
				direction: m_worldDirection,
				maxDistance: castDistance,
				layerMask: layerMask,
				hitInfo: out m_hitInfo,
				queryTriggerInteraction: QueryTriggerInteraction.Ignore);
		}

		public bool HasDetectedHit() => m_hitInfo.collider != null;
		public float GetHitDistance() => m_hitInfo.distance;
		public Vector3 GetHitNormal() => m_hitInfo.normal;
		public Vector3 GetHitPoint() => m_hitInfo.point;
		public Collider GetHitCollider() => m_hitInfo.collider;
		public Transform GetHitTransform() => m_hitInfo.transform;

		public void SetCastDirection(Vector3 rayCastDirection)
		{
			m_rayCastDirection = rayCastDirection;
		}

		public void SetCastDirection(CastDirection rayCastDirection) =>
			m_rayCastDirection = GetCastDirection(rayCastDirection);

		public void SetCastOrigin(Vector3 castOrigin)
		{
			m_bottom = m_tr.InverseTransformPoint(castOrigin);
			m_top = m_bottom + m_playerHeight * m_tr.up;
		}

		public Vector3 GetCastDirection() => m_rayCastDirection;

		public Vector3 GetCastDirection(CastDirection castDirection)
		{
			return castDirection switch
			{
				CastDirection.Up => m_tr.up,
				CastDirection.Down => -m_tr.up,
				CastDirection.Left => -m_tr.right,
				CastDirection.Right => -m_tr.right,
				_ => Vector3.one
			};
		}
	}
}