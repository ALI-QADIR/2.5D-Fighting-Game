using Smash.StructsAndEnums;
using TripleA.Extensions;
using UnityEngine;

namespace Smash.Player.CastSensors
{
	public class LedgeSensor : ICastSensor
	{
		public float lineLength;
		public LayerMask layerMask = 255;
		
		private Vector3 m_start;
		private Vector3 m_end;
		private Vector3 m_worldStart;
		private Vector3 m_worldEnd;
		private float m_maxDistance;
		
		private readonly Transform m_tr;

		private Vector3 m_rayCastDirection;

		private RaycastHit m_verticalHitInfo;
		private RaycastHit m_horizontalHitInfo;
		
		public LedgeSensor(Transform playerTR, float lineLength, float upOffset, float forwardOffset)
		{
			m_tr = playerTR;
			m_rayCastDirection = GetCastDirection(CastDirection.Down);
			this.lineLength = lineLength;
			m_maxDistance = forwardOffset + 0.2f;

			m_start = m_tr.right * forwardOffset + m_tr.up * upOffset;
			m_end = m_start - m_tr.up * this.lineLength;
		}
		
		public void Cast()
		{
			m_worldEnd = m_tr.TransformPoint(m_end);
			m_worldStart = m_tr.TransformPoint(m_start);
			m_verticalHitInfo = default;
			m_horizontalHitInfo = default;
			Physics.Linecast(
				start: m_worldStart,
				end: m_worldEnd,
				layerMask: layerMask,
				hitInfo: out m_verticalHitInfo,
				queryTriggerInteraction: QueryTriggerInteraction.Ignore);
			if (!m_verticalHitInfo.collider) return;

			Physics.Raycast(
				origin: m_tr.position.With(y: m_verticalHitInfo.point.y - 0.05f),
				direction: m_tr.right,
				maxDistance: m_maxDistance,
				layerMask: layerMask, 
				hitInfo: out m_horizontalHitInfo,
				queryTriggerInteraction: QueryTriggerInteraction.Ignore);

			/* Debug Info
			 if (HasDetectedHit())
				Debug.Log($"Hit point: {m_hitInfo.point}\n" +
					      $"Current Transform: {m_tr.position}\n" +
					      $"Hit Distance: {m_hitInfo.distance}\n" +
					      $"b:{m_bottom} ,c: {m_center}, t: {m_top}\n" +
					      $"World Bottom: {m_worldBottom}\n" +
					      $"World Top: {m_worldTop}\n" +
					      $"bottom: {m_worldBottom.Add(y: -m_playerRadius)}\n" +
					      $"top: {m_worldTop.Add(y: m_playerRadius)}\n" +
					      $"World Direction: {m_worldDirection}\n" +
					      $"Cast Direction: {m_rayCastDirection}\n" +
					      $"Cast Distance: {castDistance}\n");
			*/
		}

		public bool HasDetectedHit() => m_horizontalHitInfo.collider != null;
		public float GetHitDistance() => m_horizontalHitInfo.distance;
		public Vector3 GetHitNormal() => m_verticalHitInfo.normal;
		public Vector3 GetHitPoint() => m_horizontalHitInfo.point;
		public Collider GetHitCollider() => m_horizontalHitInfo.collider;
		public Transform GetHitTransform() => m_horizontalHitInfo.transform;
		
		public float GetHitAngle() => Vector3.Angle(m_verticalHitInfo.normal, m_tr.up);

		public void SetCastDirection(Vector3 rayCastDirection)
		{
			m_rayCastDirection = rayCastDirection;
		}

		public void SetCastDirection(CastDirection rayCastDirection) =>
			m_rayCastDirection = GetCastDirection(rayCastDirection);

		public void SetCastOrigin(Vector3 castOrigin)
		{
			m_start = castOrigin;
			m_end = m_start + GetCastDirection() * lineLength;
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