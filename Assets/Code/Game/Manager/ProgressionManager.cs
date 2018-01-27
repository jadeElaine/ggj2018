using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : IManager
{
	private List< CfgLevel > _levels = new List<CfgLevel>();
	private List< KeyValuePair< string, int > > _levelSaveMap = new List< KeyValuePair< string, int > >();
	private List< CfgFrog > _frogs = new List<CfgFrog>();
	private List< CfgTile > _tiles = new List<CfgTile> ();
	private CfgGeneral _generalConfig = null;

	public class InitData : ManagerInitData
	{
		public string[] m_assetBundleList;

		public InitData( AppManager appManager )
		{
			AppProxy.Configuration appConfig = appManager.AppProxyConfig as AppProxy.Configuration;
			
			m_assetBundleList = appConfig.m_gameConfig.m_assetBundles;
		}
	}

	#region IManager

	public CfgGeneral GetGeneralConfig()
	{
		return _generalConfig;
	}
		
	public int GetLevelCount()
	{
		return _levels.Count;
	}
	public CfgLevel GetLevelConfigByIndex( int index )
	{
		return _levels[index];
	}
	public void SaveLevel( CfgLevel data, int index )
	{
		
	}

	public int GetTileCount()
	{
		return _tiles.Count;
	}
	public CfgTile GetTileConfigByIndex (int index)
	{
		return _tiles [index];
	}

	public int GetFrogCount()
	{
		return _frogs.Count;
	}
	public CfgFrog GetFrogConfigByIndex (int index)
	{
		return _frogs [index];
	}

	public void Setup( ManagerInitData initData ) 
	{
		InitData data = initData as InitData;

		for( int i=0; i<data.m_assetBundleList.Length; ++i )
		{
			string assetBundleId = data.m_assetBundleList[i];
			Object resource = Resources.Load( assetBundleId );

			if( resource is ConfigBundle )
			{
				ParseConfigBundle( assetBundleId, resource as ConfigBundle );
			}
		}

		Debug.Log("Loaded " + _levels.Count.ToString() + " levels.");
	}

	public void Teardown()
	{
		_levels = null;
		_frogs = null;
		_tiles = null;
		_generalConfig = null;
	}
	public void UpdateFrame( float dt )
	{
	}
	public void UpdateFrameLate( float dt )
	{
	}
	public void UpdateFixed( float dt )
	{
	}

	#endregion

	void ParseConfigBundle( string assetBundleId, ConfigBundle bundle )
	{
		for( int i=0; i<bundle.m_levels.Count; ++i )
		{
			CfgLevel lcfg = bundle.m_levels[i];
			_levels.Add( lcfg );
			_levelSaveMap.Add (new KeyValuePair< string, int > (assetBundleId, i));
		}
		for( int i=0; i<bundle.m_tile.Count; ++i )
		{
			CfgTile tcfg = bundle.m_tile[i];
			_tiles.Add( tcfg );
		}
		for( int i=0; i<bundle.m_frog.Count; ++i )
		{
			CfgFrog fcfg = bundle.m_frog[i];
			_frogs.Add( fcfg );
		}
		_generalConfig = bundle.m_general;
	}
}
