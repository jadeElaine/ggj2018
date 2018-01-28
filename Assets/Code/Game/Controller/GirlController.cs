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

	public GirlController( GirlHub hub )
	{
		_hub = hub;

		Transform ct = Camera.main.transform;
		Transform gt = _hub.transform;

		ct.transform.parent.position = _hub.transform.position;
		ct.transform.position = gt.position + new Vector3 (0, 2.25f, 1);
		_initialZoom = (gt.position - ct.transform.position).magnitude;
		ct.transform.LookAt( gt );
	}

	public void UpdateFrame( float dt )
	{
		Vector3 oldPosition = _hub.transform.position;
		Vector3 mv = new Vector3(Input.GetAxis(m_horizontalWalk), 0, Input.GetAxis(m_verticalWalk)).normalized + Vector3.up*1.0f;
		_hub.GetComponent<CharacterController> ().Move( -mv * _hub.m_walkSpeed * dt );
		Vector3 newPosition = _hub.transform.position;
	}
	public void UpdateFrameLate( float dt )
	{
		Transform ct = Camera.main.transform;
		Transform gt = _hub.transform;

		Vector3 mv = new Vector3(Input.GetAxis(m_horizontalPan), 0, Input.GetAxis(m_verticalPan)).normalized;
		Transform at = ct.transform.parent;
		at.position += -mv * dt;

		_zoomLevel = Mathf.Clamp (_zoomLevel + Input.GetAxis(m_zoom)*2.5f*dt, 1.0f, 3.0f);
		ct.position = at.position + new Vector3 (0, 2.25f, 1) * _zoomLevel;

		Vector3 spread = (gt.position - at.position);
		spread.Scale( new Vector3( 1.0f, 0.0f, 1.4f ) );
		float realZoom = 1.65f * _zoomLevel;

		if (spread.magnitude > realZoom) {

			Vector3 offset = (gt.position - at.position).normalized;
			offset.Scale (new Vector3 (1.0f, 0.0f, 1.4f));
			offset *= (spread.magnitude - realZoom);
			at.position = at.position + offset;
		}
	}
}
