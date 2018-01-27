using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : IManager
{
	private UIManager _ui = null;
	private ProgressionManager _prog = null;

	private GameState _gameState = null;
	public GameState GameState { get { return _gameState; } }

	public class InitData : ManagerInitData
	{
		public AppManager m_appManager;

		public InitData( AppManager appManager )
		{
			m_appManager = appManager;
		}
	}

	#region IManager

	public void Setup( ManagerInitData initData ) 
	{
		AppManager app = (initData as InitData).m_appManager;
		_ui = app.UIManager;
		_prog = app.ProgressionManager;

		_gameState = new GameState();
	}

	public void Teardown()
	{
		_ui = null;
		_prog = null;

		_gameState = null;
	}

	public void UpdateFrame( float dt )
	{
		if( _gameState.m_currentLevel >= 0 && _gameState.m_currentLevel < _prog.GetLevelCount() )
		{
			//CfgLevel lcfg = _prog.GetLevelConfigByIndex(_gameState.m_currentLevel);

			if ( CheckLevelComplete() )
			{
				NextLevel ();
			}
		}
	}
	public void UpdateFrameLate( float dt )
	{
	}
	public void UpdateFixed( float dt )
	{
	}
		
	#endregion

	public void Init( CfgGeneral cfgGeneral )
	{
		_gameState.m_generalConfig = cfgGeneral;
		_gameState.m_currentLevel = cfgGeneral.m_startLevel - 1;
		NextLevel ();
	}

	private bool CheckLevelComplete()
	{
		return false;
	}

	public void NextLevel()
	{
		_gameState.m_currentLevel++;

		CfgLevel lcfg = _prog.GetLevelConfigByIndex( _gameState.m_currentLevel);

		AudioSource aSrc = Camera.main.GetComponent<AudioSource> ();
		aSrc.clip = lcfg.m_music;
		aSrc.Play ();

		_gameState.OnStartLevel ();

		_ui.OnLevelChange();
	}
}
