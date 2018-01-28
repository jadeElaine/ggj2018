using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHub : MonoBehaviour
{
	public float m_lifespan = 1.5f;
	public float m_maxOpacity=0.35f;
	public Renderer m_renderer;
	private float _lifeTicker=0.0f;

	public float m_sphereRadius=1.0f;
	public float m_coneDistance =5.0f;
	public float m_coneEndWidth = 1.0f;
	public float m_coneStartWidth = 0.0f;

	public FrogHub m_hostFrog = null;

	public AudioClip m_levelCompleteClip = null;

	public void OnTrigger()
	{
		m_renderer.material = new Material (m_renderer.material);

	}

	public void OnTransmit()
	{
		FrogHub[] allHubs = GameObject.FindObjectsOfType<FrogHub> ();
		for (int i = 0; i < allHubs.Length; ++i) {
			TestTransmissionObj(allHubs[i]);
		}

		BeaconHub[] allBeacons = GameObject.FindObjectsOfType<BeaconHub> ();
		for (int i = 0; i < allBeacons.Length; ++i) {
			TestTransmissionObj(allBeacons[i]);
		}
	}

	public void TestTransmissionObj(Component tob)
	{
		Vector3 spread = tob.transform.position - transform.position;
		float dist = Vector3.Dot (spread, transform.forward);
		float tangent = Vector3.Dot (spread, transform.right);

		bool valid = false;
		if (m_sphereRadius > 0 && spread.magnitude < m_sphereRadius) {
			valid = true;
		}
		if (m_coneDistance > 0 && dist > 0 && dist < m_coneDistance) {
			float ratio = dist / m_coneDistance;
			float tanSpreadAt = m_coneStartWidth * (1.0f - ratio) + m_coneEndWidth * ratio;

			if (Mathf.Abs(tangent) < tanSpreadAt) {
				valid = true;
			}
		}

		if (valid && tob != m_hostFrog) {
			if (tob is FrogHub) {
				EchoOn (tob as FrogHub);
			}
			if (tob is BeaconHub) {
				(tob as BeaconHub).OnTrigger (m_levelCompleteClip);
			}
		}
	}

	public void EchoOn( FrogHub hub )
	{
		hub.OnScream ();

		EffectHub eh = GameObject.Instantiate (hub.m_spawnedEffect, hub.transform.position, hub.transform.rotation) as EffectHub;
		eh.m_hostFrog = hub;
		eh.OnTrigger ();
	}

	void Update()
	{
		float _oldTicker = _lifeTicker;
		_lifeTicker = _lifeTicker + Time.deltaTime;

		Color c = m_renderer.material.color;

		if (_lifeTicker > 1.0f * m_lifespan) {
			Destroy (gameObject);
		}

		if (_lifeTicker > 0.75f * m_lifespan && _oldTicker <= 0.75f * m_lifespan ) {
			OnTransmit ();
		}

		if (_lifeTicker < 0.2f * m_lifespan) {
			float percent = _lifeTicker / (0.2f * m_lifespan);
			m_renderer.material.color = new Color (c.r, c.g, c.b, percent * m_maxOpacity);
		} else if (_lifeTicker > 0.65f * m_lifespan) {
			float percent = 1.0f - (_lifeTicker - 0.65f * m_lifespan) / (0.35f * m_lifespan);
			m_renderer.material.color = new Color (c.r, c.g, c.b, percent * m_maxOpacity);
		} else {
			m_renderer.material.color = new Color (c.r, c.g, c.b, m_maxOpacity);
		}
	}
}
