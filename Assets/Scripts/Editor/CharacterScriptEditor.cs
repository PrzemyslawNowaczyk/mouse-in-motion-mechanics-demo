using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CharacterScript))]
[System.Serializable] 
    public class CharacterScriptEditor : Editor {

    bool showTriggers;


    public override void OnInspectorGUI() {
        CharacterScript _target = (CharacterScript)target;


        EditorGUILayout.LabelField("Tiggers", EditorStyles.boldLabel);
        showTriggers = EditorGUILayout.Toggle("Display Triggers", showTriggers);

        if (showTriggers){
            _target.Upper = (CharacterTriggerController) EditorGUILayout.ObjectField("Upper", _target.Upper, typeof(CharacterTriggerController), false);
            _target.Lower = (CharacterTriggerController)EditorGUILayout.ObjectField("Lower", _target.Lower, typeof(CharacterTriggerController), false);
            _target.Left = (CharacterTriggerController)EditorGUILayout.ObjectField("Left", _target.Left, typeof(CharacterTriggerController), false);
            _target.Right = (CharacterTriggerController)EditorGUILayout.ObjectField("Right", _target.Right, typeof(CharacterTriggerController), false);
        }


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Gravity", EditorStyles.boldLabel);

        _target.UseGravity = EditorGUILayout.Toggle("Use Gravity", _target.UseGravity);

        if (_target.UseGravity) {
            _target.GravityModifier = EditorGUILayout.FloatField("Gravity Modifier", _target.GravityModifier);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Input", EditorStyles.boldLabel);

        _target.UsePlayerInput = EditorGUILayout.Toggle("Use Player Input", _target.UsePlayerInput);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Jump", EditorStyles.boldLabel);

        _target.UseJump = EditorGUILayout.Toggle("Use Jump", _target.UseJump);

        if (_target.UseJump) {
            _target.JumpInputExpirationTime = EditorGUILayout.FloatField("Input Expiration Time", _target.JumpInputExpirationTime);
            _target.CoyoteTime = EditorGUILayout.FloatField("Coyote Time", _target.CoyoteTime);
            _target.UpperJumpForce = EditorGUILayout.FloatField("Jump Force", _target.UpperJumpForce);
            _target.MinimalWallJumpVelocity = EditorGUILayout.FloatField("Min Walljump Speed", _target.MinimalWallJumpVelocity);
            _target.WallJumpVerticalVelocityModifier = EditorGUILayout.FloatField("Multiply vertical speed after walljump", _target.WallJumpVerticalVelocityModifier);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Bunny Hop", EditorStyles.boldLabel);

        _target.UseBunnyHop = EditorGUILayout.Toggle("Use Bunny Hop", _target.UseBunnyHop);

        if (_target.UseBunnyHop) {
            _target.BunnyHopMaxFloorContactTime = EditorGUILayout.FloatField("Accepted floor contact time", _target.BunnyHopMaxFloorContactTime);
            _target.BunnyHopMinimalVelocity = EditorGUILayout.FloatField("Minimal speed to perform bunny hop", _target.BunnyHopMinimalVelocity);
            GUIContent content = new GUIContent("Bunny hop activation tolerance", "\nBunnyHop will fire only if your horizontal \nvelocity is close to current speed level's limit.\nTolerance value determines how much is \"close\".\nIncrease it, if you have problems with firing\nbunny hops. Otherwise, keep it small.\n");
            _target.BunnyHopActivationVelocityTolerance = EditorGUILayout.FloatField(content, _target.BunnyHopActivationVelocityTolerance);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Energy transfer", EditorStyles.boldLabel);

        _target.UseEnergyTransfer = EditorGUILayout.Toggle("Use energy transfers", _target.UseEnergyTransfer);

        if (_target.UseEnergyTransfer) {
            _target.HorizontalToVerticalEnergyTransferFactor = EditorGUILayout.FloatField("Horizontal to vertical factor", _target.HorizontalToVerticalEnergyTransferFactor);
            _target.VerticalToHorizontalEnergyTransferFactor = EditorGUILayout.FloatField("Vertical to horizontal factor", _target.VerticalToHorizontalEnergyTransferFactor);
            _target.MinimalVerticalSpeedForTransfer = EditorGUILayout.FloatField("Minimal vertical speed for h2v trasfer", _target.MinimalVerticalSpeedForTransfer);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Misc", EditorStyles.boldLabel);
        _target.TotalMaxVelocity = EditorGUILayout.FloatField("Total Max Velocity", _target.TotalMaxVelocity);


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);


        SerializedProperty onLevelChanged = serializedObject.FindProperty("OnSpeedLevelChanged");

        EditorGUILayout.PropertyField(onLevelChanged);

        EditorUtility.SetDirty(this);
       
    }
}