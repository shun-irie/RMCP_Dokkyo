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

                // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
                PhotonNetwork.ConnectUsingSettings();
            }

            // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
            public override void OnConnectedToMaster()
            {
                // �w�肳�ꂽ���O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
            }

            // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
            public override void OnJoinedRoom()
            {
                // �w�肳�ꂽ�������W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
                PhotonNetwork.Instantiate(avatarPrefabName, initialPosition, Quaternion.identity);
            }
        }
    }
}
