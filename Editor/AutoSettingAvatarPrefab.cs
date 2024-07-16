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

        // Photon�t�H���_�̑��݂��`�F�b�N
        string photonFolderPath = "Assets/Photon/PhotonUnityNetworking/Resources";
        if (!Directory.Exists(photonFolderPath))
        {
            Debug.LogError($"Photon folder not found at {photonFolderPath}. Operation aborted.");
            return;
        }

        // �C�ӂ̃X�N���v�g���v���O������Ŏw��
        AttachScripts(selectedObject);

        // Prefab�����ĕۑ�
        SaveAsPrefab(selectedObject, photonFolderPath);

        Debug.Log("Scripts have been attached and the GameObject has been saved as a Prefab.");
    }

    private static void AttachScripts(GameObject obj)
    {
        // �A�^�b�`�������X�N���v�g�������ɒǉ�
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

        // �K�v�ɉ����Ă���ɃX�N���v�g��ǉ�
    }

    private static void SaveAsPrefab(GameObject obj, string folderPath)
    {
        // �t�H���_�p�X������Prefab�̃p�X���쐬
        string prefabPath = Path.Combine(folderPath, obj.name + ".prefab");

        // Prefab���쐬�܂��͍X�V
        PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);
        AssetDatabase.SaveAssets();
    }
}
