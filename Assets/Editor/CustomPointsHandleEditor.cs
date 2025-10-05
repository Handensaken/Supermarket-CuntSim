using Guard;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GuardBehaviour))]
public class CustomPointsHandleEditor : UnityEditor.Editor
{
    private void OnSceneGUI()
    {
        if (Application.isEditor)
        {
            GuardBehaviour guardBehaviour = (GuardBehaviour)target;

            Vector3 startPos = Vector3.zero;
            if (!Application.isPlaying)
            {
                startPos = guardBehaviour.transform.position;
            }
            else
            {
                startPos = guardBehaviour.StartPos;
            }


            for (int i = 0; i < guardBehaviour.PatrolPoints.Length; i++)
            {
                EditorGUI.BeginChangeCheck();

                Vector3 patrolHandlePosition =
                    Handles.PositionHandle(startPos + guardBehaviour.PatrolPoints[i], Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(guardBehaviour, "Change patrolPoint's position");
                    guardBehaviour.PatrolPoints[i] = patrolHandlePosition - startPos;
                }
            }
        }
    }
}
