using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(SubMenuSegment))]
public class SubMenuGUI : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);
        
        var segment = (SubMenuSegment) target;
        GUILayout.Label("Button Sets Start position for animation");
        if (GUILayout.Button("Set Start Position")) 
            segment.SetStartPosition();
        
        GUILayout.Label("Button Sets End position for animation");
        if (GUILayout.Button("Set End Position")) 
            segment.SetEndPosition();
    }
    

}

#endif
