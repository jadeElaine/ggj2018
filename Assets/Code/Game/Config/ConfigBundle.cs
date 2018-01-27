using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "cfg000", menuName = "Bundles/ConfigBundle", order = 1)]
[System.Serializable]
public class ConfigBundle : ScriptableObject
{
	public List< CfgLevel > m_levels = new List< CfgLevel >();
	public List< CfgFrog > m_frog = new List< CfgFrog >();
	public CfgGeneral m_general = new CfgGeneral();
}
