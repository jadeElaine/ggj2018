using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CfgTileInstance
{
	public int m_tileId;
	public int[] m_data = new int[5];

	public CfgTileInstance( int tileId )
	{
		m_tileId = tileId;
	}
}
