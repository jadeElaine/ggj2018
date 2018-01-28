using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController {

	private LevelBuilder _levelBuilder = null;
	private List< TileHub > _activeTiles = new List<TileHub>();

	private ProgressionManager _prog;
	private GameState _state;
	private int _index;

	public bool LoadLevel( ProgressionManager prog, GameState state, int index )
	{
		if (_levelBuilder != null) {
			GameObject.Destroy (_levelBuilder);
		}

		_activeTiles.Clear ();

		_prog = prog;
		_state = state;
		_index = index;

		AppManager.Instance.CoroutineRunner.StartCoroutine (ll_cr());


		return true;
	}

	private IEnumerator ll_cr()
	{
		yield return null;

		CfgLevel lcfg = _prog.GetLevelConfigByIndex (_index);
		_levelBuilder = GameObject.Instantiate( lcfg.m_content, Vector3.zero, Quaternion.identity ) as LevelBuilder;
		foreach( TileHub th in _levelBuilder.gameObject.GetComponentsInChildren<TileHub>() )
		{
			th.Init (_prog, _state);
			_activeTiles.Add( th );
		}

	}

}
