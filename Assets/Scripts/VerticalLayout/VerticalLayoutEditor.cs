//using GameCore.UI.Layout;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(VerticalLayout))]
//public class VerticalLayoutEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        var layout = (VerticalLayout)target;
//        if (GUILayout.Button("Init Layout")) layout.InitLayoutFromScene();

//        if (GUILayout.Button("Align"))
//        {
//            layout.Align();    
//        }

//        if (GUILayout.Button("Reset layout")) layout.ResetInitialState();
//    }
//}