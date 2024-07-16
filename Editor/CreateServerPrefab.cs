using UnityEngine;
using UnityEditor;

#if PHOTON_UNITY_NETWORKING
using Photon.Pun;
#else
using System.Diagnostics;
#endif

public class CreateServerPrefab : MonoBehaviour
{
    [MenuItem("RMC/Create Server Prefab")]
    static void CreateServerPrefabInHierarchy()
    {
        // Setting the path of prefab
        string prefabPath = "Assets/Prefabs/ServerPrefab.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Prefab not found at path: {prefabPath}. Please check the path and try again.");
            return;
        }

#if PHOTON_UNITY_NETWORKING
        // instantiate prefab into hierarchy
        GameObject instancePrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        // change to selected state
        Selection.activeObject = instancePrefab;

        // Completed actions
        Undo.RegisterCreatedObjectUndo(instancePrefab, "Create Server Prefab");
#else
        Debug.LogWarning("Please import PUN2 to use this feature.");
#endif
    }
}
