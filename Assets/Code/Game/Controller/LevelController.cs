using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController {

	private LevelBuilder _levelBuilder = null;
	private List< TileHub > _activeTiles = new List<TileHub>();

	public bool LoadLevel( ProgressionManager prog, GameState state, int index )
	{
		if (_levelBuilder != null) {
			GameObject.Destroy (_levelBuilder);
		}

		CfgLevel lcfg = prog.GetLevelConfigByIndex (index);
		_levelBuilder = GameObject.Instantiate( lcfg.m_content, Vector3.zero, Quaternion.identity ) as LevelBuilder;
		_activeTiles.Clear ();
		foreach( TileHub th in _levelBuilder.gameObject.GetComponentsInChildren<TileHub>() )
		{
			th.Init (prog, state);
			_activeTiles.Add( th );
		}

		return true;
	}
}
