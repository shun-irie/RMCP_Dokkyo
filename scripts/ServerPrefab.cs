using UnityEngine;
using RPC.Server;

namespace RPC
{
    public class ServerPrefab : MonoBehaviour
    {
        [SerializeField] private string roomName = "DefaultRoomName";
        [SerializeField] private string avatarPrefabName = "DefaultAvatarPrefabName";
        [SerializeField] private Vector3 initialPosition = Vector3.zero;

        private void Start()
        {
            if (PhotonServer.Instance == null)
            {
                GameObject photonServerObject = new GameObject("PhotonServer");
                PhotonServer photonServer = photonServerObject.AddComponent<PhotonServer>();
                photonServer.Connect(roomName, initialPosition, avatarPrefabName);
            }
            else
            {
                // PhotonServer�̃C���X�^���X���擾���A�ڑ����\�b�h���Ăяo��
                PhotonServer.Instance.Connect(roomName, initialPosition, avatarPrefabName);
            }
        }
    }
}
