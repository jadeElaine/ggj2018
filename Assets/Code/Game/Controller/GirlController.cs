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
	private float _initialZoom;
	private float _zoomLevel = 1.0f;
	private Vector3 _camOffset = new Vector3 ( 0.0f, 2.0f, 1.25f );
	private Vector3 _camSnapOval = new Vector3 ( 1.0f, 0.0f, 1.4f );

	private FrogHub _heldFrog = null;

	public GirlController( GirlHub hub )
	{
		_hub = hub;

		Transform ct = Camera.main.transform;
		Transform gt = _hub.transform;

		ct.transform.parent.position = _hub.transform.position;
		ct.transform.position = gt.position + _camOffset;
		_initialZoom = (gt.position - ct.transform.position).magnitude;
		ct.transform.LookAt( gt );
	}

	public void UpdateFrame( float dt )
	{
		Vector3 oldPosition = _hub.transform.position;
		Vector3 mv = new Vector3(Input.GetAxis(m_horizontalWalk), 0, Input.GetAxis(m_verticalWalk)).normalized + Vector3.up*1.0f;
		_hub.GetComponent<CharacterController> ().Move( -mv * _hub.m_walkSpeed * dt );
		Vector3 newPosition = _hub.transform.position;

		if ((newPosition - oldPosition).sqrMagnitude > Mathf.Epsilon) {
			Vector3 facing = (newPosition - oldPosition).normalized;
			_hub.transform.rotation.SetLookRotation (facing, Vector3.up);
		}

		if (Input.GetButton (m_interact)) {
			if (_heldFrog == null) {
				// find frogs
				FrogHub[] _allHubs = GameObject.FindObjectsOfType<FrogHub>();
				FrogHub pickup = null;
				float pickupDistance = 0.0f;

				for (int i = 0; i < _allHubs.Length; ++i) {
					float dist = (_allHubs [i].transform.position - _hub.transform.position).magnitude;
					if (dist < _allHubs [i].m_pickupRange && _allHubs [i].m_canPickUp) {
						if (pickup == null || dist < pickupDistance) {
							pickup = _allHubs [i];
							pickupDistance = dist;
						}
					}
				}

				if (pickup != null) {
					_heldFrog.transform.SetParent (_hub.m_frogBone);
					_heldFrog.transform.localPosition = Vector3.zero;
					_heldFrog.transform.localRotation = Quaternion.identity;
					_heldFrog.OnPickUp ();
					_heldFrog = pickup;
				}

			} else {
				Transform ground = _hub.transform.parent;
				_heldFrog.transform.SetParent (ground);
				_heldFrog.transform.position = _hub.transform.position + _hub.transform.forward * 0.1f;
				_heldFrog.transform.rotation = _hub.transform.rotation;
				_heldFrog.OnDrop ();
				_heldFrog = null;
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
		float realZoom = 1.65f * _zoomLevel;

		if (spread.magnitude > realZoom) {

			Vector3 offset = (gt.position - at.position).normalized;
			offset.Scale( _camSnapOval );
			offset *= (spread.magnitude - realZoom);
			at.position = at.position + offset;
		}
	}
}
