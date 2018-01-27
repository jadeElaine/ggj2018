using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TileHub))]
public class TileGate : MonoBehaviour 
{
	private TileHub _host;

	public void OnInit( TileHub host )
	{
		_host = host;
	}
}
