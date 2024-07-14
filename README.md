# RMCP API
This is an API document for using the RMC protocol.
## namespace: RMC
### namespace: RMC.Server
#### Class: PhotonServer
```csharp
public void Connect(string roomName, Vector3 initialPosition, string avatarPrefabName)
```
**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
|string| roomName | room name that the user will log in.|
|Vector3|initialPosition|a global position where an avatar prefab is instantiated.|
|string|avatarPrefabName| a name of avatar prefab file that is instantiated. The file must be included in Assets/Photon/PhotonUnityNetworking/Resources

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
**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| Quaternion | rotation | the quaternion of a bone|

**Output**
| type | description                         |
| ---- | ----------------------------------- |
| byte[]| byte array converted from a quaternion of joint rotation|
```csharp
public static Quaternion ByteArrayToQuaternion(byte[] byteArray, int offset)
```
Converting the byte array of a bone rotation to a Quaternion array.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| byte[] | byteArray | joint rotation byte array calculated by QuaternionToByteArray|
| int | offset | offset for reading byte array |

**Output**
| type | description                         |
| ---- | ----------------------------------- |
| Quaternion | a quaternion of bone rotation converted from a byte array |

```csharp
public static byte[] Vector3ToByteArray(Vector3 vector)
```
Converting the position data to a byte array.

**Input**
| type | parameter| description                         |
| ---- | -------- | ----------------------------------- |
| Vector3 | vector | a global position of root position of an avatar|

**Output**
| type | description                         |
| ---- | ----------------------------------- |
|byte[]|a byte array converted from a global position vector|

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
The function extracted the rotations of all bones from the animator. However, the avatar must be humanoid.
```csharp
public static void SetBoneRotationsFromByteArray(Animator animator, byte[] rotationsByteArray)
```
The function updates avatar bones from a bone byte array.
```csharp
public static Animator InitializeAnimator(string objectName, out float timePerFrame, float framesPerSecond)
```
Initialize animator and update frequency
```csharp
public static void UpdateAnimator(PhotonView photonView, Animator sourceAnimator, ref float timer, float timePerFrame, string rpcMethodName)
```
The function processes the synchronization of avatar movement across users.
```csharp
public static void SetAnimatorStateFromByteArray(Animator animator, Transform root, byte[] data)
```
