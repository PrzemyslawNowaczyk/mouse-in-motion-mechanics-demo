using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterScript : MonoBehaviour {

    public CharacterTriggerController Upper, Lower, Left, Right;

    public float GravityModifier;
    public float TotalMaxVelocity;

    public bool UseGravity;
    public bool UsePlayerInput;
    public bool UseJump;
    public bool UseBunnyHop;
    public bool UseEnergyTransfer;

    public SpeedLevel[] SpeedLevels;


    public float JumpInputExpirationTime;

    public float CoyoteTime = 0.13f;    //reference to Willy the Coyote's ability to run in the air for a while. Couldn't find more accurate term. Describes how long after leaving ground collider, player is still able to jump.
    public float UpperJumpForce = 35.0f;
    public float MinimalWallJumpVelocity = 40.0f;
    public float BunnyHopMaxFloorContactTime = 0.3f;
    public float HorizontalToVerticalEnergyTransferFactor = 0.75f;
    public float VerticalToHorizontalEnergyTransferFactor = 1.0f;
    public float MinimalVerticalSpeedForTransfer = 3.0f;
    public float BunnyHopActivationVelocityTolerance = 1.0f;
    public float BunnyHopMinimalVelocity = 15.0f;
    public float WallJumpVerticalVelocityModifier = 0.75f;

    private Rigidbody _rigidbody;
    private KeyboardInputHandler _inputHandle;
    private CharacterInputState _currentInputState;
    private float _timeSinceLastJumpInput = float.MaxValue;
    private readonly CharacterInputState _emptyInputState = new CharacterInputState();
    private bool _withinCoyoteTimeWindow;
    private int _bunnyHopCounter;
    private int _currentSpeedLevelNumber;


    public SpeedLevel CurrentSpeedLevel {
        get { return (SpeedLevels[CurrentSpeedLevelNumber]); }
    }

    public int CurrentSpeedLevelNumber {
        get { return _currentSpeedLevelNumber; }
        private set {
            _currentSpeedLevelNumber = value;
            OnSpeedLevelChanged.Invoke(value, SpeedLevels[value]);
        }
    }

    public Vector3 LocalVelocity {
        get { return GlobalVectorToLocal(_rigidbody.velocity); }
    }

    public Vector3 GlobalVelocity {
        get { return _rigidbody.velocity; }
    }

    public void SetExternalVelocity(Vector3 velocity) {
        _rigidbody.velocity = velocity;

        RecalculateSpeedLevel(LocalVelocity.x);
    }



    public SpeedLevelChangedEvent OnSpeedLevelChanged;



    private void Awake() {
        _inputHandle = new KeyboardInputHandler("Move Left", "Move Right", "Jump");
        _currentInputState = new CharacterInputState();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start () {
        _rigidbody.useGravity = false;
        CurrentSpeedLevelNumber = 0;
    }

    private void Update () {
        _currentInputState = _inputHandle.HandleInput();
        UpdateJumpInputTime(ref _timeSinceLastJumpInput);
    }

    private void UpdateJumpInputTime(ref float _timeSinceLastJumpInput) {
        if (_currentInputState.jumpPressed) {
            _timeSinceLastJumpInput = 0.0f;
        }
        else {
            _timeSinceLastJumpInput += Time.deltaTime;
        }
    }

    private void FixedUpdate() {

        CharacterInputState inputState;

        if (UseGravity) {
            ApplyGravity();
        }

        if (UsePlayerInput) {
            inputState = _currentInputState;
        }
        else {
            inputState = _emptyInputState;
        }

        ApplyInput(inputState);

        if (UseJump) {
            ApplyJump(inputState);
        }

        ApplySpeedLevelReduction();

        LimitTotalVelocity();
    }

    private void ApplyGravity() {
        _rigidbody.AddForce(Physics.gravity * GravityModifier, ForceMode.Acceleration);
    }

    private void ApplyInput(CharacterInputState inputState) {

        float forceModifier = Lower.IsColliding ? 1.0f : CurrentSpeedLevel.MidairForceMultiplier;

        if (InputMatchesVelocity(inputState)) {
            Accelerate(forceModifier, inputState.directionalInput);
        }
        else {

            if (InputAgainstVelocity(inputState)) {
                Brake(forceModifier);
            }
            else {
                LoseSpeed(forceModifier);
            }

        }
    }

    private void ApplyJump(CharacterInputState inputState) {

        if (Lower.IsColliding && !Lower.LastFrameWasColliding) {
            _withinCoyoteTimeWindow = true;
        }
        if (Lower.TimeSinceLeftCollision > CoyoteTime) {
            _withinCoyoteTimeWindow = false;
        }

        if (!JumpInputExpired()) {

            if (UpperJumpRequirementsMet()) {
                SetLocalVelocityParam(UpperJumpForce, Coord3.Y); //jump
                _withinCoyoteTimeWindow = false;
                _timeSinceLastJumpInput = JumpInputExpirationTime + CoyoteTime; //making jump input expire

                bool withinBunnyHopWindow;

                if (Lower.IsColliding && Lower.CollidingTime <= BunnyHopMaxFloorContactTime) {
                    withinBunnyHopWindow = true;
                }
                else {
                    withinBunnyHopWindow = false;
                }

                if (UseBunnyHop && withinBunnyHopWindow) {
                    ApplyBunnyHop(inputState);
                    _bunnyHopCounter++;
                }
                else {
                    _bunnyHopCounter = 0;
                }

            }
            else {
                if (Right.IsColliding) {
                    PerformWallJump(LeftOrRight.Left);
                }
                if (Left.IsColliding) {
                    PerformWallJump(LeftOrRight.Right);
                }
            }
        }

    }

    private void ApplyBunnyHop(CharacterInputState inputState) {

        float direction = Mathf2.Sign3(LocalVelocity.x);

        if (StrongBunnyHopRequirementsMet(inputState)) {
            SetLocalVelocityParam(direction * SpeedLevels[CurrentSpeedLevelNumber + 1].HorizontalSpeedLimit - (BunnyHopActivationVelocityTolerance / 2.0f), Coord3.X);
            CurrentSpeedLevelNumber++;
        }
        else if (WeakBunnyHopRequirementsMet(inputState)) { //weaker version of bunny hop is performed to make sure that next jump will fulfill requirements
            SetLocalVelocityParam(direction * SpeedLevels[CurrentSpeedLevelNumber].HorizontalSpeedLimit - (BunnyHopActivationVelocityTolerance / 2.0f), Coord3.X);
        }

    }

    private void ApplySpeedLevelReduction() {
        if (CurrentSpeedLevelNumber > 0 && Mathf.Abs(LocalVelocity.x) < CurrentSpeedLevel.LowerSpeedBoundary) {
            CurrentSpeedLevelNumber--;
        }
    }

    private void LimitTotalVelocity() {
        if (_rigidbody.velocity.magnitude > TotalMaxVelocity) {
            _rigidbody.velocity = _rigidbody.velocity.normalized * TotalMaxVelocity;
        }
    }

    private void Accelerate(float forceModifier, float direction) {

        direction = Mathf2.Sign3(direction);

        if (BelowCurrentSpeedLimit()) {  //if we're below speed limit
            _rigidbody.AddForce(transform.right * direction * CurrentSpeedLevel.MovementForceBelowLimit * forceModifier + Vector3.right * direction * -_rigidbody.velocity.y * 0.005f * CurrentSpeedLevel.MovementForceBelowLimit);
        }
        else {                         //if we're not
            _rigidbody.AddForce(transform.right * direction * CurrentSpeedLevel.MovementForceAboveLimit * forceModifier);
        }
    }

    private void Brake(float forceModifier) {
        float direction = Mathf2.Sign3(_rigidbody.velocity.x);
        _rigidbody.AddForce(transform.right * -direction * CurrentSpeedLevel.ChangeDirectionForce * forceModifier);
    }

    private void LoseSpeed(float forceModifier) {
        _rigidbody.AddForce(transform.right * _rigidbody.velocity.x * CurrentSpeedLevel.NoInputForceModifier * forceModifier);
    }

    private bool InputMatchesVelocity(CharacterInputState inputState) {
            return ((Mathf2.Sign3(_rigidbody.velocity.x) == Mathf2.Sign3(inputState.directionalInput)) && inputState.directionalInput != 0.0f) 
            || (_rigidbody.velocity.x == 0.0f && inputState.directionalInput != 0.0f);
    }

    private bool InputAgainstVelocity(CharacterInputState inputState) {
        return (inputState.directionalInput != 0.0f && (Mathf2.Sign3(_rigidbody.velocity.x) == -Mathf2.Sign3(inputState.directionalInput)));
    }

    private bool BelowCurrentSpeedLimit() {
            return (Mathf.Abs(LocalVelocity.x) < SpeedLevels[CurrentSpeedLevelNumber].HorizontalSpeedLimit);
    }

    private void PerformWallJump(LeftOrRight side) {

        _timeSinceLastJumpInput = JumpInputExpirationTime + CoyoteTime;

        float newHorizontalSpeed = 0;

        if (UseEnergyTransfer) {
            newHorizontalSpeed = Mathf.Abs(LocalVelocity.y) * VerticalToHorizontalEnergyTransferFactor;
            newHorizontalSpeed = Mathf.Max(newHorizontalSpeed, MinimalWallJumpVelocity);
        }
        else {
            newHorizontalSpeed = MinimalWallJumpVelocity;
        }

        if (side == LeftOrRight.Left) {
            SetLocalVelocityParam(-newHorizontalSpeed, Coord3.X);
        }
        else {
            SetLocalVelocityParam(newHorizontalSpeed, Coord3.X);
        }

        SetLocalVelocityParam(LocalVelocity.y * WallJumpVerticalVelocityModifier, Coord3.Y);

        RecalculateSpeedLevel(LocalVelocity.x);

    }

    private enum LeftOrRight {
        Left, Right
    }

    private bool UpperJumpRequirementsMet() {
        return Lower.IsColliding || _withinCoyoteTimeWindow;
    }

    private bool JumpInputExpired() {
        return _timeSinceLastJumpInput > JumpInputExpirationTime;
    }

    private bool StrongBunnyHopRequirementsMet(CharacterInputState inputState) {
        return Mathf.Abs(LocalVelocity.x ) > (CurrentSpeedLevel.HorizontalSpeedLimit - BunnyHopActivationVelocityTolerance)
            && (CurrentSpeedLevelNumber + 1) < SpeedLevels.Length
            && Mathf2.Sign3(inputState.directionalInput) == Mathf2.Sign3(LocalVelocity.x);
    }

    private bool WeakBunnyHopRequirementsMet(CharacterInputState inputState) {
        return Mathf.Abs(LocalVelocity.x) >= BunnyHopMinimalVelocity && Mathf2.Sign3(inputState.directionalInput) == Mathf2.Sign3(LocalVelocity.x);
    }

    private void OnCollisionEnter(Collision collision) {
        if (UseEnergyTransfer) {

            var impact = Mathf.Abs(GlobalVectorToLocal(collision.relativeVelocity).x);
            if (!Lower.IsColliding && (Left.IsColliding || Right.IsColliding) && impact > 0.1f) {
                if (LocalVelocity.y > MinimalVerticalSpeedForTransfer) {
                    SetLocalVelocityParam(LocalVelocity.y + impact * HorizontalToVerticalEnergyTransferFactor, Coord3.Y);
                }
            }

        }
    }

    private void RecalculateSpeedLevel(float speed) {
        for (int i = 0; i < SpeedLevels.Length; i++) {
            if (Mathf.Abs(speed) > SpeedLevels[i].HorizontalSpeedLimit) {
                CurrentSpeedLevelNumber = i;
            }
        }
    }

    private Vector3 GlobalVectorToLocal(Vector3 vector) {
        return transform.InverseTransformDirection(vector);
    }

    private void SetLocalVelocityParam(float value, Coord3 coord) {
        var localVelocity = LocalVelocity;
        switch (coord) {
            case Coord3.X:
                localVelocity.x = value;
                break;
            case Coord3.Y:
                localVelocity.y = value;
                break;
            case Coord3.Z:
                localVelocity.z = value;
                break;
            default:
                break;
        }
        _rigidbody.velocity = transform.TransformDirection(localVelocity);
    }

    enum Coord3 { X, Y, Z }

    [Serializable]
    public class SpeedLevel {
        public float HorizontalSpeedLimit;
        public float LowerSpeedBoundary;
        public float MovementForceBelowLimit;
        public float MovementForceAboveLimit;
        public float ChangeDirectionForce;
        public float NoInputForceModifier;
        public float MidairForceMultiplier;
        public Gradient ColorGradient;
    }

    [Serializable]
    public class SpeedLevelChangedEvent : UnityEvent<int, SpeedLevel> {
    }
}
