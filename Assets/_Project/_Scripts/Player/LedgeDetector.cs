using Smash.Player.CastSensors;
using Smash.StructsAndEnums;
using UnityEngine;

namespace Smash.Player
{
	public class LedgeDetector : MonoBehaviour
	{
		[SerializeField] private float m_height = 1.8f;
		[SerializeField] private float m_radius = 0.35f;
		[SerializeField] private float m_lineHeight = 0.35f;
		[SerializeField] private float m_upOffset = 0.35f;
		[SerializeField] private float m_forwardOffset = 0.35f;
		[SerializeField] private bool m_debug;

		private LedgeSensor m_sensor;
		private Transform m_tr;
		
		private int m_currentLayer;
		private bool m_isLedgeDetected;

		private void Awake()
		{
			m_tr ??= transform;
			RecalibrateSensor();
		}

		public void CheckForLedge()
		{
			if (m_currentLayer != gameObject.layer)
			{
				RecalculateSensorLayerMask();
			}
			
			m_sensor.Cast();
			m_isLedgeDetected = m_sensor.HasDetectedHit();
		}
		
		public bool IsLedgeDetected() => m_isLedgeDetected;

		public void SetOnLedge()
		{
			// fix positioning
			Vector3 position = m_sensor.GetHitPoint();
			position -= m_tr.right * m_radius;
			position -= m_tr.up * m_height;
			m_tr.position = position;
		}

		public void ResetSensorHits()
		{
			m_isLedgeDetected = false;
			m_sensor.ResetHits();
		}

		#region Private Methods

		private void RecalibrateSensor()
		{
			m_sensor ??= new LedgeSensor(m_tr, m_lineHeight, m_height + m_upOffset, m_radius + m_forwardOffset);
			m_sensor.SetCastDirection(CastDirection.Down);
		    
			RecalculateSensorLayerMask();

			m_sensor.lineLength = m_lineHeight;
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

#if UNITY_EDITOR
		
		private Vector3 m_worldStart, m_worldEnd;
		private void OnDrawGizmos()
		{
			if(!m_debug) return;
			Vector3 start = transform.right * (m_forwardOffset + m_radius) + transform.up * (m_upOffset + m_height);
			Vector3 end = start - transform.up * m_lineHeight;
			m_worldStart = transform.TransformPoint(start);
			m_worldEnd= transform.TransformPoint(end);
			Gizmos.DrawLine(m_worldStart, m_worldEnd);
			Gizmos.color = IsLedgeDetected() ? Color.red : Color.green;
		}
#endif

		#endregion Private Methods
	}
}
