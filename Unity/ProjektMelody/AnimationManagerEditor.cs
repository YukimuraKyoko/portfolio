using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions; // needed for Regex

[CustomEditor(typeof(AnimationManager))]
public class AnimationManagerEditor : Editor
{

    AnimationManager AM;
    
    SerializedProperty temp;
    SerializedProperty anim;
    SerializedProperty param;
    SerializedProperty keys;

    bool manualSettings = false;
    bool simpleSettings = false;

    void OnEnable()
    {

        temp = serializedObject.FindProperty("temp"); //string[] temp;
        anim = serializedObject.FindProperty("animator"); //Animator animator;
        param = serializedObject.FindProperty("parameters"); //string[] parameters;
        keys = serializedObject.FindProperty("keys"); //KeyCode[] keys;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.indentLevel = 0;
        serializedObject.Update();
        EditorGUILayout.Space();
        //base.OnInspectorGUI();
        AM = (AnimationManager)target;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Parameters", GUILayout.Height(20)))
        {
            //Uncomment line below to bring up old version of the animationkeybind window
            //AnimationKeyBind.OpenWindow(AM);


            temp.arraySize = AM.animator.parameterCount;
            param.arraySize = AM.animator.parameterCount;
            //Debug.Log(param.arraySize);
            //Fill and display parameters
            for (int i = 0; i < AM.parameters.Length; i++)
            {
                param.GetArrayElementAtIndex(i).stringValue = AM.animator.parameters[i].name;

            }
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Reset Parameters", GUILayout.Height(20)))
        {
            
            AM.temp = new string[AM.animator.parameterCount];
            AM.parameters = new string[AM.animator.parameterCount];
            param.arraySize = AM.parameters.Length;
            serializedObject.ApplyModifiedProperties();

            Debug.Log("AM.temp.length: " + AM.temp.Length + "\n" + 
                "AM.parameters.length: " + AM.parameters.Length); 
        }

        if(GUILayout.Button("Reset Keys", GUILayout.Height(20)))
        {
            AM.keys = new KeyCode[AM.animator.parameterCount];
            keys.arraySize = AM.keys.Length;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Open Settings", GUILayout.Height(20)))
        {
            manualSettings = !manualSettings;
        }
        /*
        if (GUILayout.Button("Simple Settings", GUILayout.Height(20)))
        {
            simpleSettings = !simpleSettings;
        }
        */
        EditorGUILayout.Space();
        

        //Show and Apply Animator Input box
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(anim, false);
        EditorGUI.EndChangeCheck();
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Temporary Note: \n" +
            "Currently this manager works only if each parameter is assigned to only 1 unique button. \n" +
            "Eg. Can't have Two 'A' buttons in the slots. \n" +
            "When using another Animator in the slot above, click Reset Parameters, Reset Keys, and then Generate Parameters. \n" +
            "\n" +
            "The keys must be consistently assigned from top to bottom, can't leave any of them as none.", MessageType.Info);



        if (manualSettings)
        {
            DisplayDoubleArrays();
        }
        /*
        if (simpleSettings)
        {
            DisplayAllKeyInputBoxes();
        }
        */

       

    }





    void DisplayAllKeyInputBoxes()
    {
        for (int i = 0; i < AM.temp.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            //Display Key input box
            AM.temp[i] = EditorGUILayout.TextField(AM.temp[i], GUILayout.Width(30));

            //Force input string to the first character
            if (AM.temp[i].Length > 1)
            {
                AM.temp[i] = AM.temp[i].Substring(0, 1);
            }

            //Force input string to only be Uppercase
            if (AM.temp[i] != AM.temp[i].ToUpper())
            {
                AM.temp[i] = AM.temp[i].ToUpper();
            }


            EditorGUI.EndChangeCheck();
            //Restrict Key input box to only A-Z, 0-9 keys
            AM.temp[i] = Regex.Replace(AM.temp[i], @"[^A-Z0-9]", "");
            temp.GetArrayElementAtIndex(i).stringValue = AM.temp[i];
            AM.keys[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), AM.temp[i]);


            serializedObject.ApplyModifiedProperties();
            //Display Parameter name
            GUILayout.Label(AM.parameters[i]);
            EditorGUILayout.EndHorizontal();
        }
    }



    void DisplayDoubleArrays()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical();
            for (int i = 0; i <param.arraySize; i++)
            {
                
                EditorGUILayout.PropertyField(param.GetArrayElementAtIndex(i), GUIContent.none);
            }
            EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical();
        for (int i = 0; i < keys.arraySize; i++)
        {

            EditorGUILayout.PropertyField(keys.GetArrayElementAtIndex(i), GUIContent.none);
        }
        EditorGUILayout.EndVertical();


        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();
    }

}



//Unused for now
public class AnimationKeyBind : EditorWindow
{
    
    
    static AnimationKeyBind window;
    static AnimationManager AM2;
    public static void OpenWindow(AnimationManager AM)
    {
        AM2 = AM;
        window = (AnimationKeyBind)GetWindow(typeof(AnimationKeyBind));
        window.minSize = new Vector2(400, 600);
        window.Show();
    }

    void OnGUI()
    {
        //Source Animator Input
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Animator");
        AM2.animator = (Animator)EditorGUILayout.ObjectField(AM2.animator, typeof(Animator), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Generate Parameters", GUILayout.Height(20)))
        {
            GenerateParameters();

        }

        //Debug.Log(AM2.parameters[0]);
    }


    void GenerateParameters()
    {
        for (int i = 0; i < AM2.parameters.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(AM2.parameters[i]);
            //AM2.animator = (Animator)EditorGUILayout.ObjectField(AM2.animator, typeof(Animator), true);
            EditorGUILayout.EndHorizontal();
        }
    }



}
