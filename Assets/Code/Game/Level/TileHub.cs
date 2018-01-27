using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileExtension
{
	void OnInit (TileHub host);
}

public class TileHub : MonoBehaviour
{
	private ProgressionManager _prog=null;
	private GameState _state=null;
	private CfgTile _sharedData=null;
	private CfgTileInstance _instanceData=null;

	public ProgressionManager Prog { get { return _prog; } }
	public GameState State { get { return _state; } }
	public CfgTile SharedData { get { return _sharedData; } }
	public CfgTileInstance InstanceData { get { return _instanceData; } }

	public void Init( ProgressionManager prog, GameState state, CfgTile sharedData, CfgTileInstance instanceData )
	{
		_prog = prog;
		_state = state;
		_sharedData = sharedData;
		_instanceData = instanceData;

		ITileExtension[] its = GetComponents<ITileExtension> ();
		for (int i = 0; i < its.Length; ++i) {
			its[i].OnInit( this );
		}
	}
}
