using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MenuPage))]
public class MenuPageEditor : Editor {

	public override void OnInspectorGUI()
    {
        MenuPage page = (MenuPage)target;

        page.background = EditorGUILayout.ObjectField("Background: ",page.background,typeof(Sprite),false) as Sprite;
    }
}
