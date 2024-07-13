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
            // PhotonServerのインスタンスを取得し、接続メソッドを呼び出す
            PhotonServer.Instance.Connect(roomName, initialPosition, avatarPrefabName);
        }
    }
}
