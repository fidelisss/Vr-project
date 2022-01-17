using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(SubMenuSystem))]
public class SubMenuSystemGUI : Editor
{

    
    private SubMenuSystem _system;

    private void OnEnable()
    {
        _system = (SubMenuSystem) target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginDisabledGroup(_system.IsSerializeObject == false);
        GUILayout.Label("Trigger Object");
        EditorGUI.EndDisabledGroup();
        
        EditorGUI.BeginDisabledGroup(_system.IsXRHand == false);
        GUILayout.Label("XR Hand");
        EditorGUI.EndDisabledGroup();
        
        GUILayout.Space(20);
        DrawDefaultInspector();
        GUILayout.Space(20);
    }
    
}
#endif