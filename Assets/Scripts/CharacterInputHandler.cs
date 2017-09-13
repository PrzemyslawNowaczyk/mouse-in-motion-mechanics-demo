using UnityEngine;

public abstract class CharacterInputHandler {

    public abstract CharacterInputState HandleInput();
}


public struct CharacterInputState {
    public float directionalInput;
    public bool jumpPressed;

    public CharacterInputState(float directional, bool jump) {
        directionalInput = directional;
        jumpPressed = jump;
    }

}


public class KeyboardInputHandler : CharacterInputHandler{

    private string moveLeftButton;
    private string moveRightButton;
    private string jumpButton;

    public KeyboardInputHandler(string moveLeftBtn, string moveRightBtn, string jumpBtn) {
        moveLeftButton = moveLeftBtn;
        moveRightButton = moveRightBtn;
        jumpButton = jumpBtn;
    }

    public override CharacterInputState HandleInput() {
        CharacterInputState state = new CharacterInputState();

        if (Input.GetButton(moveLeftButton)) {
            state.directionalInput -= 1.0f;
        }
        if (Input.GetButton(moveRightButton)) {
            state.directionalInput += 1.0f;
        }
        if (Input.GetButtonDown(jumpButton)) {
            state.jumpPressed = true;
        }

        return state;
    }
}