using Smash.StructsAndEnums;
using UnityEngine;

namespace Smash.Player
{
	public interface ICastSensor
	{
		void Cast();
		bool HasDetectedHit();
		float GetHitDistance();
		Vector3 GetHitNormal();
		Vector3 GetHitPoint();
		Collider GetHitCollider();
		Transform GetHitTransform();
		void SetCastDirection(Vector3 rayCastDirection);
		void SetCastDirection(CastDirection rayCastDirection);
		void SetCastOrigin(Vector3 castOrigin);
		Vector3 GetCastDirection();
	}
}