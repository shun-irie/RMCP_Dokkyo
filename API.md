# RMCP API document
This is an API document for using the RMC protocol.
## namespace: RMC
### namespace: RMC.Server
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
### namespace: RMC.MoCap
#### Class: calc_funcs
```csharp
public static byte[] QuaternionToByteArray(Quaternion rotation)
```
Converting the Quaternion array of a bone rotation to a byte array.
```csharp
public static Quaternion ByteArrayToQuaternion(byte[] byteArray, int offset)
```
Converting the byte array of a bone rotation to a Quaternion array.
```csharp
public static byte[] Vector3ToByteArray(Vector3 vector)
```
Converting the position data to a byte array.
```csharp
public static Vector3 ByteArrayToVector3(byte[] byteArray, int offset)
```
Converting the byte array to a position vector data.
```csharp
public static string[] bones
```
The list includes all bone names to be synchronized across users.
```csharp
public static HumanBodyBones ConvertStringToHumanBodyBone(string boneName)
```
Converting the bone names to the parameter of HumanBodyBones
```csharp
public static byte[] GetBoneRotationsAsByteArray(Animator animator)
```

```csharp
public static void SetBoneRotationsFromByteArray(Animator animator, byte[] rotationsByteArray)
```

```csharp
public static Animator InitializeAnimator(string objectName, out float timePerFrame, float framesPerSecond)
```

```csharp
public static void UpdateAnimator(PhotonView photonView, Animator sourceAnimator, ref float timer, float timePerFrame)
```
