using UnityEngine;
using UnityEngine.Animations;
using Normal.Realtime;

public class PlayerManager : MonoBehaviour {
    [SerializeField] private GameObject _camera = default;
    [SerializeField] private GameObject _prefab;

    private Realtime _realtime;

    private void Awake() {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Callback when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;

        // Option to connect to room in offline mode, dont require Internet, for single play, or quick test feature
        // Update in 2.10.0
        _realtime.Connect("room_id", new Room.ConnectOptions {
            offlineMode = true
        });
    }

    private void DidConnectToRoom(Realtime realtime) {
        Realtime.InstantiateOptions options = new Realtime.InstantiateOptions { 
            ownedByClient = true,
            preventOwnershipTakeover = false,
            destroyWhenOwnerLeaves = true,
            destroyWhenLastClientLeaves = true,
            useInstance = realtime
        };
        
        // Instantiate the Player for this client once we've successfully connected to the room
        GameObject playerGameObject = Realtime.Instantiate(prefabName: _prefab.name, options);

        // Get a reference to the player
        Player player = playerGameObject.GetComponent<Player>();

        // Get the constraint used to position the camera behind the player
        ParentConstraint cameraConstraint = _camera.GetComponent<ParentConstraint>();
        
        // Add the camera target so the camera follows it
        ConstraintSource constraintSource = new ConstraintSource { sourceTransform = player.cameraTarget, weight = 1.0f };
        int constraintIndex = cameraConstraint.AddSource(constraintSource);

        // Set the camera offset so it acts like a third-person camera.
        cameraConstraint.SetTranslationOffset(constraintIndex, new Vector3( 0.0f,  1.0f, -4.0f));
        cameraConstraint.SetRotationOffset   (constraintIndex, new Vector3(15.0f,  0.0f,  0.0f));
    }
}
