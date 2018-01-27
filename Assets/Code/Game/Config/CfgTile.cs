using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CfgTile
{
	public enum DefaultMode
	{
		None=0,
		Border=1,
		Center=2,
	};

	public string m_name;
	public TileHub m_prefab;
	public DefaultMode m_default;
}
