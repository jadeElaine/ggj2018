using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TileHub))]
public class TileGirlSpawn : MonoBehaviour
{
	private TileHub _host;

	[SerializeField]
	private MeshRenderer _editRenderer;

	public void OnInit( TileHub host )
	{
		_host = host;

		_editRenderer.enabled = !_host.State.EditMode;
	}
}
