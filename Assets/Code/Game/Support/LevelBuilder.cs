using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

	public TileHub[] m_prefabs=null;
	public int m_borderPrefab=1;
	public int m_centerPrefab=0;

	public int m_width=11;
	public int m_height=11;

	[SerializeField]
	private List<TileHub> _tiles = new List<TileHub>();

	public TileHub GetTile( int x, int y )
	{
		int index = y * m_width + x;
		return _tiles [index];
	}

	public bool IsGenerated() {
		return _tiles.Count != 0;
	}

	public void RebuildEmpty()
	{
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		List< Transform > killList = new List<Transform> ();
		foreach (Transform t in gameObject.transform) {
			killList.Add (t);
		}
		for( int i=0; i<killList.Count; ++i )
		{
			GameObject.DestroyImmediate (killList[i].gameObject);
		}

		_tiles.Clear ();
		for (int y = 0; y < m_height; ++y) {
			for (int x = 0; x < m_width; ++x) { 
				bool isBorder = x == 0 || y == 0 || x == m_width - 1 || y == m_height - 1;
				Vector3 pos = new Vector3 (x, 0, y);
				TileHub prefab = isBorder ? m_prefabs[m_borderPrefab] : m_prefabs[m_centerPrefab];
				TileHub th = GameObject.Instantiate (prefab, pos, Quaternion.identity) as TileHub;
				th.transform.SetParent (transform);
				_tiles.Add (th);
			}
		}

	}

	public void ReplaceTile( int x, int y, int prefabIndex )
	{
		int index = y * m_width + x;
		DestroyImmediate (_tiles [y * m_width + x].gameObject);
		Vector3 pos = new Vector3 (x, 0, y);
		TileHub prefab = m_prefabs[prefabIndex];
		TileHub th = GameObject.Instantiate (prefab, pos, Quaternion.identity) as TileHub;
		th.transform.SetParent (transform);
		_tiles[index] = th;
	}
}
