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

		if (host.State.m_girlController != null) {
			Debug.LogError ("2 GIRLS???? FREAK OUT!");
		} else {
			GirlHub gh = GameObject.Instantiate (host.Prog.GetGeneralConfig ().m_girlPrefab, transform) as GirlHub;

			host.State.m_girlController = new GirlController (gh);
		}
	}
}
