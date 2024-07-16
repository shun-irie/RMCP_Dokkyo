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
        // �v���n�u�̃p�X�𖾎��I�Ɏw��
        string prefabPath = "Assets/Prefabs/ServerPrefab.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Prefab not found at path: {prefabPath}. Please check the path and try again.");
            return;
        }

#if PHOTON_UNITY_NETWORKING
        // �q�G�����L�[�Ƀv���n�u�̃C���X�^���X�𐶐�
        GameObject instancePrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        // ���������I�u�W�F�N�g��I����Ԃɂ���
        Selection.activeObject = instancePrefab;

        // �A�N�V�����̊�����o�^
        Undo.RegisterCreatedObjectUndo(instancePrefab, "Create Server Prefab");
#else
        Debug.LogWarning("Please import PUN2 to use this feature.");
#endif
    }
}
