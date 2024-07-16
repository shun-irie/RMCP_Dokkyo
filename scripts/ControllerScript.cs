using UnityEngine;
using Photon.Pun;

public class ControllerScript : MonoBehaviour
{
    // The list of monobehaviours should be removed in remote places.
    public string[] motionCaptureScripts;

    void Start()
    {
        // PhotonView instance
        PhotonView photonView = GetComponent<PhotonView>();

        // PhotonView is not user's
        if (!photonView.IsMine)
        {
            // Get all components
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            // For all target monobehaviours
            foreach (string motionCaptureScript in motionCaptureScripts)
            {
                Debug.Log($"Checking for script: {motionCaptureScript}");

                // Checking components
                foreach (var script in scripts)
                {
                    Debug.Log($"Found script: {script.GetType().Name} (enabled: {script.enabled})");

                    // if the name of component is matched to a target, it should be disabled
                    if (script.GetType().Name == motionCaptureScript)
                    {
                        Debug.Log($"Disabling script: {motionCaptureScript}");
                        script.enabled = false;
                        Debug.Log($"Script {motionCaptureScript} disabled: {script.enabled}");
                    }
                }
            }
        }
    }
}
