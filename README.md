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
```csharp
public override void OnConnectedToMaster()
```
A callback while successfully connected with the master server.
```csharp
public override void OnJoinedRoom()
```
A callback while successfully connected with the game server.
### namespace: RPC.MoCap
#### Class: calc_funcs
```csharp
public static byte[] QuaternionToByteArray(Quaternion rotation)
```
Converting the Quaternion array of a bone to a byte array.
