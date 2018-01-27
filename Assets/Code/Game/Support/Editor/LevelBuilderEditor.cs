using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor
{
	private Dictionary< string, int > _tileKeyIndexMap = new Dictionary<string, int>();

	public override void OnInspectorGUI()
	{
		LevelBuilder lb = target as LevelBuilder;

		if (GUILayout.Button ("Regenerate Empty")) {
			lb.RebuildEmpty ();
		}

		if (lb.IsGenerated ()) {
			string ttlist = "";
			_tileKeyIndexMap.Clear ();
			for (int i = 0; i < lb.m_prefabs.Length; ++i) {
				ttlist += lb.m_prefabs [i].m_displayKey;
				_tileKeyIndexMap [lb.m_prefabs [i].m_displayKey] = i;
			}
			EditorGUILayout.LabelField ("TileTypes: " + ttlist);

			GUILayout.Space (20);
			GUILayout.BeginVertical ();
			for( int y=0; y<lb.m_height; ++y )
			{
				GUILayout.BeginHorizontal ();
				for( int x=0; x<lb.m_width; ++x )
				{
					string letter = lb.GetTile (x, y).m_displayKey;
					string newLetter = GUILayout.TextField (letter, GUILayout.Width (20));
					if (newLetter != letter) {
						string truncated = newLetter.Substring (newLetter.Length - 1).ToUpper ();
						if (_tileKeyIndexMap.ContainsKey (truncated)) {
							lb.ReplaceTile (x, y, _tileKeyIndexMap [truncated]);
						}
					}
				}
				GUILayout.EndHorizontal ();
			}
			GUILayout.EndVertical();	
		}
	}
}
