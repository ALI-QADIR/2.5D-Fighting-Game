using Smash.StructsAndEnums;
using UnityEngine;

namespace Smash.Player.CastSensors
{
	public class RaycastCastSensor : ICastSensor
	{
		public float castDistance;
		public LayerMask layerMask = 255;
		
		private Vector3 m_origin;
		private Vector3 m_worldOrigin;
		private Vector3 m_worldDirection;

		private readonly Transform m_tr;

		private Vector3 m_rayCastDirection;

		private RaycastHit m_hitInfo;

		public RaycastCastSensor(Transform playerTR, CastDirection rayCastDirection, Vector3 origin)
		{
			m_tr = playerTR;
			m_rayCastDirection = GetCastDirection(rayCastDirection);
			m_origin = origin;
		}
		
		public void Cast()
		{
			m_worldOrigin = m_origin;
			m_worldDirection = GetCastDirection();
			
			Physics.Raycast(
				origin: m_worldOrigin,
				direction: m_worldDirection,
				hitInfo: out m_hitInfo,
				maxDistance: castDistance,
				layerMask: layerMask,
				queryTriggerInteraction: QueryTriggerInteraction.Ignore);
		}
		
		public void ResetHits() => m_hitInfo = default;
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

		public void SetCastOrigin(Vector3 castOrigin) => m_origin = castOrigin;
		
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