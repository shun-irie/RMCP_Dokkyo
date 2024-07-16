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
        // プレハブのパスを明示的に指定
        string prefabPath = "Assets/Prefabs/ServerPrefab.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Prefab not found at path: {prefabPath}. Please check the path and try again.");
            return;
        }

#if PHOTON_UNITY_NETWORKING
        // ヒエラルキーにプレハブのインスタンスを生成
        GameObject instancePrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        // 生成したオブジェクトを選択状態にする
        Selection.activeObject = instancePrefab;

        // アクションの完了を登録
        Undo.RegisterCreatedObjectUndo(instancePrefab, "Create Server Prefab");
#else
        Debug.LogWarning("Please import PUN2 to use this feature.");
#endif
    }
}
