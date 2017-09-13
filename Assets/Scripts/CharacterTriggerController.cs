using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerController : MonoBehaviour {

    public bool IsColliding { get { return _collision; } }
    public float CollidingTime { get; private set; }
    public float TimeSinceLeftCollision { get; private set; }
    public bool LastFrameWasColliding { get; private set; }
    public LayerMask CollisionMask;

    bool _collision = false;

    BoxCollider _collider;

    private void Awake() {
        _collision = false;
        _collider = GetComponent<BoxCollider>();
    }


    private void FixedUpdate() {
        LastFrameWasColliding = _collision;

        //this is kinda weird solution, but checking collisions in FixedUpdate gives somewhat better control over execution order.
        if (Physics.CheckBox(transform.TransformPoint(_collider.center), _collider.size, transform.rotation, CollisionMask)){
            CollidingTime += Time.fixedDeltaTime;
            _collision = true;
            TimeSinceLeftCollision = 0.0f;
        }
        else {
            _collision = false;
            CollidingTime = 0.0f;
            TimeSinceLeftCollision += Time.fixedDeltaTime;
        }
    }


}
