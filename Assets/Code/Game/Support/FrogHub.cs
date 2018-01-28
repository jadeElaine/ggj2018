using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHub : MonoBehaviour {

	public float m_screamDelay = 0.75f;
	public float m_screamDistance = 5.0f;
	public float m_screamDuration = 1.0f;
	public float m_screamReload = 0.25f;
	public float[] m_screamDirections;

	public bool m_canPickUp = true;
	public float m_pickupRange = 1.0f;

	public Transform m_screamBone;
	public Transform m_lowerJawBone;
	public Transform m_upperJawBone;
	public Transform m_leftBrowBone;
	public Transform m_rightBrowBone;

	public AudioClip[] m_audioScreams;
	public AudioClip[] m_audioPickups;
	public AudioClip[] m_audioDrops;

	private AudioSource _source;

	void Start()
	{
		_source = GetComponent<AudioSource> ();
	}

	public void OnScream()
	{
		if( m_audioScreams.Length == 0 ) return;
		int index = UnityEngine.Random.Range( 0, m_audioScreams.Length );
		if (_source.isPlaying) { _source.Stop (); }
		_source.clip = m_audioScreams [index];
		_source.Play ();
	}

	public void OnPickUp()
	{
		if( m_audioPickups.Length == 0 ) return;
		int index = UnityEngine.Random.Range( 0, m_audioPickups.Length );
		if (_source.isPlaying) { _source.Stop (); }
		_source.clip = m_audioPickups [index];
		_source.Play ();
	}

	public void OnDrop()
	{
		if( m_audioDrops.Length == 0 ) return;
		int index = UnityEngine.Random.Range( 0, m_audioDrops.Length );
		if (_source.isPlaying) { _source.Stop (); }
		_source.clip = m_audioDrops [index];
		_source.Play ();
	}
}
