using UnityEditor;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class AutoSettingAvatarPrefab : Editor
{
    [MenuItem("RMC/Auto setting avatar prefab")]
    private static void AutoSettingAvatarPrefabMenuItem()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.LogError("No GameObject selected. Please select a GameObject in the hierarchy.");
            return;
        }

        // Photonフォルダの存在をチェック
        string photonFolderPath = "Assets/Photon/PhotonUnityNetworking/Resources";
        if (!Directory.Exists(photonFolderPath))
        {
            Debug.LogError($"Photon folder not found at {photonFolderPath}. Operation aborted.");
            return;
        }

        // 任意のスクリプトをプログラム上で指定
        AttachScripts(selectedObject);

        // Prefab化して保存
        SaveAsPrefab(selectedObject, photonFolderPath);

        Debug.Log("Scripts have been attached and the GameObject has been saved as a Prefab.");
    }

    private static void AttachScripts(GameObject obj)
    {
        // アタッチしたいスクリプトをここに追加
        if (!obj.GetComponent<RMCprotocol>())
        {
            obj.AddComponent<RMCprotocol>();
        }

        if (!obj.GetComponent<ControllerScript>())
        {
            obj.AddComponent<ControllerScript>();
        }
        if (!obj.GetComponent<PhotonView>())
        {
            obj.AddComponent<PhotonView>();
        }

        // 必要に応じてさらにスクリプトを追加
    }

    private static void SaveAsPrefab(GameObject obj, string folderPath)
    {
        // フォルダパスを元にPrefabのパスを作成
        string prefabPath = Path.Combine(folderPath, obj.name + ".prefab");

        // Prefabを作成または更新
        PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);
        AssetDatabase.SaveAssets();
    }
}
