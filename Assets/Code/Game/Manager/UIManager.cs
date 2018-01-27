﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHub
{
	void OnLevelChange( AppManager app );
	void UI( AppManager app );
}

public class UIManager : IManager
{
	private AppManager _app = null;
	private List<IHub> _globalHubs = null;

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
		InitData data = initData as InitData;
		_app = data.m_appManager;
		
		_globalHubs = new List<IHub>();
		MonoBehaviour[] allMonobehaviours = GameObject.FindObjectsOfType<MonoBehaviour>();
		foreach( MonoBehaviour mb in allMonobehaviours )
		{
			if( mb is IHub )
			{
				_globalHubs.Add( mb as IHub );
			}
		}
	}

	public void Teardown()
	{
		_globalHubs.Clear();
		_app = null;
	}

	public void UpdateFrame( float dt )
	{
		for( int i=0; i<_globalHubs.Count; ++i )
		{
			_globalHubs[i].UI( _app );
		}
	}
	public void UpdateFrameLate( float dt )
	{
	}
	public void UpdateFixed( float dt )
	{
	}

	#endregion

	public void OnLevelChange()
	{
		for( int i=0; i<_globalHubs.Count; ++i )
		{
			_globalHubs[i].OnLevelChange(_app);
		}
	}

	public void PlayMovieEffect( string effectId )
	{
		for (int i = 0; i < _globalHubs.Count; ++i) 
		{
			if( _globalHubs[i] is UIHubMovieEffect )
			{
				UIHubMovieEffect hme = _globalHubs [i] as UIHubMovieEffect;
				if (hme.name == effectId)
				{
					hme.Play ();
				}
			}
		}
	}
}
