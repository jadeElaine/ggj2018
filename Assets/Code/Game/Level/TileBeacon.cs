﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TileHub))]
public class TileBeacon : MonoBehaviour, ITileExtension
{
//	private TileHub _host;

	public void OnInit( TileHub host )
	{
//		_host = host;
	}
}
