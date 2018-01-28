using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconHub : MonoBehaviour
{
	private bool _isTriggered = false;
	private bool _isDone = false;
	public float _ticker = 0.0f;
	public void OnTrigger( AudioClip clip )
	{
		if (!_isTriggered) {

			AudioSource src = GetComponent<AudioSource> ();
			if (src == null) {
				src=  gameObject.AddComponent<AudioSource> ();
			}
			src.clip = clip;
			src.Play ();
			_isTriggered = true;
		}
	}

	void Update()
	{
		if (_isTriggered) {
			_ticker += Time.deltaTime;
		}

		if (_ticker > 1.5f && !_isDone) {
			AppManager.Instance.GameStateManager.NextLevel ();
			_isDone = true;
		}
	}
}
