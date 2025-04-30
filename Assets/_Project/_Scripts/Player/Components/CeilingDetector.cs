using Smash.Player.CastSensors;
using Smash.StructsAndEnums;
using UnityEngine;

namespace Smash.Player.Components
{
	public class CeilingDetector : MonoBehaviour
	{
		[SerializeField] private float m_height = 1.8f;
		
		private RaycastSensor m_sensor;
		private Collider m_col;
		private Transform m_tr;
		
		private int m_currentLayer;
		private bool m_isCeilingDetected;
		private const float k_SafetyDistance = 0.015f;
		private float m_baseSensorRange;
		
		private void Awake()
		{
			m_tr ??= transform;
			m_col ??= GetComponent<Collider>();
			RecalibrateSensor();
		}

		public void CheckForCeiling()
		{
			if (m_currentLayer != gameObject.layer)
			{
				RecalculateSensorLayerMask();
			}

			m_sensor.castDistance = m_baseSensorRange;
			m_sensor.SetCastOrigin(m_col.bounds.center);
			
			m_sensor.Cast();
		    
			m_isCeilingDetected = m_sensor.HasDetectedHit();
		}

		public void ResetSensorHits()
		{
			m_isCeilingDetected = false;
			m_sensor.ResetHits();
		}
		
		public bool IsCeilingDetected() => m_isCeilingDetected;
		
		private void RecalibrateSensor()
		{
			m_sensor ??= new RaycastSensor(m_tr, CastDirection.Up, m_col.bounds.center);
			m_sensor.SetCastOrigin(m_col.bounds.center);
			m_sensor.SetCastDirection(CastDirection.Up);
		    
			RecalculateSensorLayerMask();

			m_baseSensorRange = m_height * 0.6f + k_SafetyDistance;
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
	}
}
