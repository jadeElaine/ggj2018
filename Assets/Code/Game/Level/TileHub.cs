using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileExtension
{
	void OnInit (TileHub host);
}

public class TileHub : MonoBehaviour
{
	public string m_displayKey;

	private ProgressionManager _prog=null;
	private GameState _state=null;

	public ProgressionManager Prog { get { return _prog; } }
	public GameState State { get { return _state; } }

	public void Init( ProgressionManager prog, GameState state )
	{
		_prog = prog;
		_state = state;

		ITileExtension[] its = GetComponents<ITileExtension> ();
		for (int i = 0; i < its.Length; ++i) {
			its[i].OnInit( this );
		}
	}
}
