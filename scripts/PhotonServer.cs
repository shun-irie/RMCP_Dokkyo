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

                // Connecting to master server using PhotonServerSettings
                PhotonNetwork.ConnectUsingSettings();
            }

            // Callback for succeeding to connect to a master server
            public override void OnConnectedToMaster()
            {
                // connecting to room
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
            }

            // Callback for succeeding to connect to a game server
            public override void OnJoinedRoom()
            {
                // instantiate the avatar prefab to initialPosition
                PhotonNetwork.Instantiate(avatarPrefabName, initialPosition, Quaternion.identity);
            }
        }
    }
}
