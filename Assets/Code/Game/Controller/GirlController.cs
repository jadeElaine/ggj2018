using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController {

	public string m_horizontalWalk = "Horizontal";
	public string m_verticalWalk = "Vertical";
	public string m_horizontalPan = "HorizontalPan";
	public string m_verticalPan = "VerticalPan";
	public string m_zoom = "Zoom";
	public string m_interact = "PickUp";

	private GirlHub _hub;
//	private float _initialZoom;
	private float _zoomLevel = 1.0f;
	private Vector3 _camOffset = new Vector3 ( 0.0f, 2.0f, 1.25f );
	private Vector3 _camSnapOval = new Vector3 ( 1.0f, 0.0f, 1.0f );

	private AudioSource _source;

	private FrogHub _heldFrog = null;

	public GirlController( GirlHub hub )
	{
		_hub = hub;
		_source = _hub.GetComponent<AudioSource> ();

		Transform ct = Camera.main.transform;
		Transform gt = _hub.transform;

		ct.transform.parent.position = _hub.transform.position;
		ct.transform.position = gt.position + _camOffset;
//		_initialZoom = (gt.position - ct.transform.position).magnitude;
		ct.transform.LookAt( gt );
	}

	public void UpdateFrame( float dt )
	{
		Vector3 mv = new Vector3(Input.GetAxis(m_horizontalWalk), 0, Input.GetAxis(m_verticalWalk)).normalized;
		Vector3 force = mv + Vector3.up*1.0f;
		_hub.GetComponent<CharacterController> ().Move( -force * _hub.m_walkSpeed * dt );

		Animator anim = _hub.GetComponent<Animator> ();
		anim.speed = _hub.m_animSpeed;

		if (mv.sqrMagnitude > Mathf.Epsilon) {
			_hub.transform.LookAt (_hub.transform.position - mv, Vector3.up);
			anim.SetFloat ("walkSpeed", 1.0f);
		} else {
			anim.SetFloat ("walkSpeed", 0.0f);
		}

		if (Input.GetButtonDown (m_interact)) {
			if (_heldFrog == null) {
				// find note
				NoteHub[] _allNotes = GameObject.FindObjectsOfType<NoteHub> ();
				NoteHub note = null;
				for (int i = 0; i < _allNotes.Length; ++i) {
					float dist = (_allNotes [i].transform.position - _hub.transform.position).magnitude;
					if (dist < _allNotes [i].m_triggerRange) {
						note = _allNotes [i];
					}
				}

				if (note != null) {
					EffectHub eh = GameObject.Instantiate (note.m_spawnedEffect, note.transform.position, note.transform.rotation) as EffectHub;
					eh.OnTrigger ();

					if (_source.isPlaying) {
						_source.Stop ();
					}
					_source.clip = _hub.m_noteAudio;
					_source.Play ();
				} else {

					// find frogs
					FrogHub[] _allHubs = GameObject.FindObjectsOfType<FrogHub> ();
					FrogHub pickup = null;
					float pickupDistance = 0.0f;

					for (int i = 0; i < _allHubs.Length; ++i) {
						float dist = (_allHubs [i].transform.position - _hub.transform.position).magnitude;
						if (dist < _allHubs [i]._pickupRange && _allHubs [i].m_canPickUp) {
							if (pickup == null || dist < pickupDistance) {
								pickup = _allHubs [i];
								pickupDistance = dist;
							}
						}
					}

					if (pickup != null) {
						_heldFrog = pickup;
						_heldFrog.transform.SetParent (_hub.m_frogBone);
						_heldFrog.transform.localPosition = Vector3.zero;
						_heldFrog.transform.localRotation = Quaternion.identity;
						_heldFrog.OnPickUp ();
						anim.SetBool ("hasFrog", true);
					}
				}
			} else {
				Transform ground = _hub.transform.parent;
				_heldFrog.transform.SetParent (ground);
				_heldFrog.transform.position = _hub.transform.position + _hub.transform.forward * 0.1f;
				_heldFrog.transform.rotation = _hub.transform.rotation;
				_heldFrog.OnDrop ();
				_heldFrog = null;
				anim.SetBool ("hasFrog", false);
			}
		}
	}
	public void UpdateFrameLate( float dt )
	{
		Transform ct = Camera.main.transform;
		Transform gt = _hub.transform;

		Vector3 mv = new Vector3(Input.GetAxis(m_horizontalPan), 0, Input.GetAxis(m_verticalPan)).normalized;
		Transform at = ct.transform.parent;
		at.position += -mv * dt;

		_zoomLevel = Mathf.Clamp (_zoomLevel + Input.GetAxis(m_zoom)*2.5f*dt, 1.0f, 3.0f);
		ct.position = at.position + _camOffset * _zoomLevel;

		Vector3 spread = (gt.position - at.position);
		spread.Scale( _camSnapOval );
		float realZoom = 1.0f * _zoomLevel;

		if (spread.magnitude > realZoom) {

			Vector3 offset = (gt.position - at.position).normalized;
			offset.Scale( _camSnapOval );
			offset *= (spread.magnitude - realZoom);
			at.position = at.position + offset;
		}
	}
}
