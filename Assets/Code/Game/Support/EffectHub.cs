using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHub : MonoBehaviour
{
	public float m_lifespan = 1.5f;
	public float m_maxOpacity=0.35f;
	public Renderer m_renderer;
	public Collider m_collider;
	private float _lifeTicker=0.0f;

	public List< Collider > _intersections = new List<Collider>();

	public void OnTrigger()
	{
		m_renderer.material = new Material (m_renderer.material);

	}

	public void OnTransmit()
	{
		Debug.Log ("Other Colliders!" + _intersections.Count.ToString ());
	}

	public void OnTriggerEnter( Collider Other )
	{
		_intersections.Add (Other);
	}

	void Update()
	{
		float _oldTicker = _lifeTicker;
		_lifeTicker = _lifeTicker + Time.deltaTime;

		Color c = m_renderer.material.color;

		if (_lifeTicker > 1.0f * m_lifespan) {
			Destroy (gameObject);
		}

		if (_lifeTicker > 0.75f * m_lifespan && _oldTicker <= 0.75f) {
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
