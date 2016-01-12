// Copyright 2011 Sven Magnus

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(UniTileManager))]

public class UniTileManagerEditor : Editor {
	
	private int selectedTemplate;
	
	public override void OnInspectorGUI () {
		UniTileManager manager = this.target as UniTileManager;
		
		if(this.target.name!="UniTileManager") this.target.name="UniTileManager";
		
		Rect rect = EditorGUILayout.BeginVertical();
		EditorGUILayout.EndVertical();

		if (GUILayout.Button("Add tile layer")) 
		{
			manager.layerCount++;
			GameObject g = new GameObject("Layer " + manager.layerCount);
			TileLayer tl = g.AddComponent<TileLayer>();
			if(manager.lastLayer!=null) tl.material = manager.lastLayer.material;
			// todo: add other properties
			manager.lastLayer = tl;
		}
		
		if (GUILayout.Button("Add object layer")) 
		{
			manager.objectLayerCount++;
			GameObject g = new GameObject("Object Layer " + manager.objectLayerCount);
			g.AddComponent<ObjectLayer>();			
		}
	}
	
}
