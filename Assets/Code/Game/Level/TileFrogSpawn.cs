using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TileHub))]
public class TileFrogSpawn : MonoBehaviour
{
	private TileHub _host;

	[SerializeField]
	private MeshRenderer _editRenderer;

	public void OnInit( TileHub host )
	{
		_host = host;

		if (!_host.State.EditMode) {
			_editRenderer.enabled = false;
		}
	}
}
