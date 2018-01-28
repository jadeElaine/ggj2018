using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TileHub))]
public class TileFrogSpawn : MonoBehaviour
{
	private TileHub _host;

	public int m_frogType;

	[SerializeField]
	private MeshRenderer _editRenderer;

	public void OnInit( TileHub host )
	{
		_host = host;

		_editRenderer.enabled = false;

		CfgFrog fc = host.Prog.GetFrogConfigByIndex (m_frogType);
		FrogHub fh = GameObject.Instantiate (fc.m_prefab, transform) as FrogHub;

		host.State.m_activeFrogs.Add (new FrogController (fh));
	}
}
