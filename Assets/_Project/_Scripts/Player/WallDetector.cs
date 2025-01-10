using Smash.Player.CastSensors;
using Smash.StructsAndEnums;
using UnityEngine;

namespace Smash.Player
{
	public class WallDetector : MonoBehaviour
	{
		[SerializeField] private float m_radius = 0.35f;
		[SerializeField] private float m_extension = 0.05f;

		private RaycastSensor m_sensor;
		private Transform m_tr;
		private Collider m_col;

		private int m_currentLayer;
		private bool m_isWallDetected;
		private const float k_SafetyDistance = 0.015f;
		private float m_baseSensorRange;

		private void Awake()
		{
			m_tr ??= transform;
			m_col ??= GetComponent<Collider>();
			RecalibrateSensor();
		}

		public void CheckForWall()
		{
			ResetHits();
			
			if (m_currentLayer != gameObject.layer)
			{
				RecalculateSensorLayerMask();
			}
			// Debug.Log("Checking for wall");
			m_sensor.castDistance = m_baseSensorRange;
			m_sensor.SetCastDirection(m_tr.right);
			m_sensor.SetCastOrigin(m_col.bounds.center);
			
			m_sensor.Cast();
			m_isWallDetected = m_sensor.HasDetectedHit();
		}
		
		public bool IsWallDetected() => m_isWallDetected;

		public void ResetSensorHits()
		{
			m_isWallDetected = false;
			m_sensor.ResetHits();
		}

		#region Private Methods
		
		private void RecalibrateSensor()
		{
			m_sensor ??= new RaycastSensor(
				playerTR: m_tr,
				rayCastDirection: CastDirection.Right,
				origin: m_col.bounds.center
			);
			
			RecalculateSensorLayerMask();
			
			m_baseSensorRange = m_radius + k_SafetyDistance + m_extension;
			m_sensor.castDistance = m_baseSensorRange;
		}

		private void RecalculateSensorLayerMask()
		{
			int objectLayer = gameObject.layer;
			int layerMask = Physics.AllLayers;
		    
			for (int i = 0; i < 32; i++)
			{
				if (Physics.GetIgnoreLayerCollision(objectLayer, i))
					layerMask &= ~(1 << i);
			}
		    
			int ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
			layerMask &= ~(1 << ignoreLayer);
		    
			m_sensor.layerMask = layerMask;
			m_currentLayer = layerMask;
		}

		private void ResetHits() => m_sensor.ResetHits();

		#endregion Private Methods
	}
}
