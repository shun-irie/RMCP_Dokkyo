using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace RPC
{
    namespace Server
    {
        public class PhotonServer : MonoBehaviourPunCallbacks
        {
            public static PhotonServer Instance;

            [SerializeField] private string roomName = "Room";
            [SerializeField] private Vector3 initialPosition = Vector3.zero;
            [SerializeField] private string avatarPrefabName = "AvatarPrefab";

            private void Awake()
            {
                if (Instance == null)
                {
                    Instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            public void Connect(string roomName, Vector3 initialPosition, string avatarPrefabName)
            {
                this.roomName = roomName;
                this.initialPosition = initialPosition;
                this.avatarPrefabName = avatarPrefabName;

                // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
                PhotonNetwork.ConnectUsingSettings();
            }

            // マスターサーバーへの接続が成功した時に呼ばれるコールバック
            public override void OnConnectedToMaster()
            {
                // 指定された名前のルームに参加する（ルームが存在しなければ作成して参加する）
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
            }

            // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
            public override void OnJoinedRoom()
            {
                // 指定された初期座標に自身のアバター（ネットワークオブジェクト）を生成する
                PhotonNetwork.Instantiate(avatarPrefabName, initialPosition, Quaternion.identity);
            }
        }
    }
}
