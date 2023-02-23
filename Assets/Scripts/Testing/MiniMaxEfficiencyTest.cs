using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MiniMaxEfficiencyTest : MonoBehaviour
{
    [SerializeField]
    private int maxDepth = 10;

    private static readonly Board b = new();
    private static int iterations;
    private static int index = 1;

    private void StartTest()
    {
        iterations = maxDepth;
        runNext.Invoke(new(0, 0, 0, 0), 0);
    }

    private static readonly MiniMax.Then runNext = (v, f) =>
    {
        if (index <= iterations)
        {
            MiniMax.Evaluate(b, new CountScoring(), 1, index++, runNext);
        }
    };

#if UNITY_EDITOR
    [CustomEditor(typeof(MiniMaxEfficiencyTest))]
    public class MiniMaxEfficiencyTestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MiniMaxEfficiencyTest myScript = (MiniMaxEfficiencyTest)target;
            if (GUILayout.Button("Start Test"))
            {
                if (Application.isPlaying) // only run test in play mode
                    myScript.StartTest();
                else
                    Debug.LogWarning("This test can only be run in Play Mode.");
            }
        }
    }
#endif
}
