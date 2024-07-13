using UnityEngine;
using Photon.Pun;

public class ControllerScript : MonoBehaviour
{
    // 対象のスクリプト名を保持するリスト
    public string[] motionCaptureScripts;

    // スタート時に呼び出されるメソッド
    void Start()
    {
        // PhotonViewを取得
        PhotonView photonView = GetComponent<PhotonView>();

        // このPhotonViewが自分のものでない場合
        if (!photonView.IsMine)
        {
            // すべてのコンポーネントを取得
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            // 各ターゲットスクリプト名について
            foreach (string motionCaptureScript in motionCaptureScripts)
            {
                // すべてのコンポーネントをチェック
                foreach (var script in scripts)
                {
                    // コンポーネントの名前がターゲットスクリプト名と一致する場合、それを無効化
                    if (script.GetType().Name == motionCaptureScript)
                    {
                        script.enabled = false;
                    }
                }
            }
        }
    }
}
