using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CfgLevel
{
	public AudioClip m_music;

	public int m_width=12;
	public int m_height=12;
	public CfgTileInstance[] m_tiles = null;
}
