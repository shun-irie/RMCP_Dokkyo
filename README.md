# RMCP_Dokkyo
RMC protocol from Dokkyo Medical University
## namespace: RPC
### namespace: RPC.Server
#### Class: PhotonServer
```csharp
public void Connect(string roomName, Vector3 initialPosition, string avatarPrefabName)
```
roomName: Room name in the photon environment
initialPosition: a global position where an avatar prefab is instantiated.
avatarPrefabName: a name of avatar prefab file that is instantiated. The file must be included in Assets/Photon/PhotonUnityNetworking/Resources
