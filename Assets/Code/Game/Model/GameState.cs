using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
	public CfgGeneral m_generalConfig = null; 

	private bool _editMode = false;
	public bool EditMode { get { return _editMode; } }

	public int m_currentLevel = -1;

	public GirlController m_girlController = null;
	public List< FrogController > m_activeFrogs = new List< FrogController >();

	public GameState( bool editMode )
	{
		_editMode = editMode;
	}

	//public void OnStartLevel( CfgLevel level )
	//{
	//	
	//}
}
