using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController {

	private GirlHub _hub;

	public GirlController( GirlHub hub )
	{
		_hub = hub;

		Transform ct = Camera.main.transform;
		Transform gt = _hub.transform;
		ct.transform.position = gt.position + new Vector3 (0, 2.25f, 1);
		ct.transform.LookAt( gt );
	}

	public void UpdateFrame( float dt )
	{
	}
	public void UpdateFrameLate( float dt )
	{
		Transform ct = Camera.main.transform;

	}
}
