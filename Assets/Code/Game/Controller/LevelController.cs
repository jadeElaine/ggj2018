using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController {

	private List< TileHub > _activeTiles;

	public bool LoadLevel( ProgressionManager prog, GameState state, int index )
	{
		CfgLevel lcfg = prog.GetLevelConfigByIndex (index);

		if (lcfg.m_tiles == null || lcfg.m_tiles.Length == 0) {
			if (state.EditMode == true) {
				lcfg.m_tiles = new CfgTileInstance[lcfg.m_width * lcfg.m_height];
				for (int y = 0; y < lcfg.m_height; ++y) {
					for (int x = 0; x < lcfg.m_width; ++x) {
						int tileId = 0;
						CfgTile.DefaultMode mode = CfgTile.DefaultMode.Center;
						if (y == 0 || x == 0 || y == lcfg.m_height - 1 || x == lcfg.m_width - 1) {
							mode = CfgTile.DefaultMode.Border;
						}
						for (int i = 0; i < prog.GetTileCount(); ++i) {
							CfgTile t = prog.GetTileConfigByIndex (i);
							if (t.m_default == mode) {
								tileId = i;
								break;
							}
						}
						lcfg.m_tiles [y * lcfg.m_width + x] = new CfgTileInstance(tileId);
					}
				}
				prog.SaveLevel (lcfg, index);
			} else {
				return false;
			}
		}

		for (int y = 0; y < lcfg.m_height; ++y) {
			for (int x = 0; x < lcfg.m_width; ++x) {
				CfgTileInstance ti = lcfg.m_tiles [y * lcfg.m_width + x];
				CfgTile t = prog.GetTileConfigByIndex (ti.m_tileId);

				TileHub hub = GameObject.Instantiate (t.m_prefab, new Vector3 (x, 0, y), Quaternion.identity) as TileHub;
				hub.Init (prog, state, t, ti);
			}
		}

		return true;
	}
}
