using UnityEngine;
using Photon.Pun;

public class ControllerScript : MonoBehaviour
{
    // �Ώۂ̃X�N���v�g����ێ����郊�X�g
    public string[] motionCaptureScripts;

    // �X�^�[�g���ɌĂяo����郁�\�b�h
    void Start()
    {
        // PhotonView���擾
        PhotonView photonView = GetComponent<PhotonView>();

        // ����PhotonView�������̂��̂łȂ��ꍇ
        if (!photonView.IsMine)
        {
            // ���ׂẴR���|�[�l���g���擾
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            // �e�^�[�Q�b�g�X�N���v�g���ɂ���
            foreach (string motionCaptureScript in motionCaptureScripts)
            {
                // ���ׂẴR���|�[�l���g���`�F�b�N
                foreach (var script in scripts)
                {
                    // �R���|�[�l���g�̖��O���^�[�Q�b�g�X�N���v�g���ƈ�v����ꍇ�A����𖳌���
                    if (script.GetType().Name == motionCaptureScript)
                    {
                        script.enabled = false;
                    }
                }
            }
        }
    }
}
