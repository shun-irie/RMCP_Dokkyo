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

        // Checking whether photon resource folder was existed
        string photonFolderPath = "Assets/Photon/PhotonUnityNetworking/Resources";
        if (!Directory.Exists(photonFolderPath))
        {
            Debug.LogError($"Photon folder not found at {photonFolderPath}. Operation aborted.");
            return;
        }

        // Attaching scripts
        AttachScripts(selectedObject);

        // Save an avatar as a prefab file
        SaveAsPrefab(selectedObject, photonFolderPath);

        Debug.Log("Scripts have been attached and the GameObject has been saved as a Prefab.");
    }

    private static void AttachScripts(GameObject obj)
    {
        // Add scripts here
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

        // It is possible to add the scripts if necessary
    }

    private static void SaveAsPrefab(GameObject obj, string folderPath)
    {
        // Create the path
        string prefabPath = Path.Combine(folderPath, obj.name + ".prefab");

        // Create and update prefab file
        PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);
        AssetDatabase.SaveAssets();
    }
}
