using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInputHandler : MonoBehaviour
{
    [Tooltip("Sensitivity multiplier for moving the camera around")]
    public float lookSensitivity = 1f;
    [Tooltip("Additional sensitivity multiplier for WebGL")]
    public float webglLookSensitivityMultiplier = 0.25f;
    [Tooltip("Limit to consider an input when using a trigger on a controller")]
    public float triggerAxisThreshold = 0.4f;
    [Tooltip("Used to flip the vertical input axis")]
    public bool invertYAxis = false;
    [Tooltip("Used to flip the horizontal input axis")]
    public bool invertXAxis = false;

    GameFlowManager m_GameFlowManager;
    PlayerCharacterController m_PlayerCharacterController;
    bool m_FireInputWasHeld;

    private void Start()
    {
        m_PlayerCharacterController = GetComponent<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, PlayerInputHandler>(m_PlayerCharacterController, this, gameObject);
        m_GameFlowManager = FindObjectOfType<GameFlowManager>();
        DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, PlayerInputHandler>(m_GameFlowManager, this);

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void LateUpdate()
    {
        m_FireInputWasHeld = GetFireInputHeld();
    }

    public bool CanProcessInput()
    {
        return true;// Cursor.lockState == CursorLockMode.Locked && !m_GameFlowManager.gameIsEnding;
    }

    public Vector3 GetMoveInput()
    {
        if (CanProcessInput())
        {
            float xPos = ControlFreak2.CF2Input.GetAxis(GameConstants.k_AxisNameHorizontal);
            float zPos = ControlFreak2.CF2Input.GetAxis(GameConstants.k_AxisNameVertical);
            //Vector3 move = new Vector3(ControlFreak2.CF2Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f, ControlFreak2.CF2Input.GetAxisRaw(GameConstants.k_AxisNameVertical));
            Vector3 move = new Vector3(xPos, 0f, zPos);
            //Debug.Log("Pos - x: " + xPos + " , z: " + zPos);
            // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }

        return Vector3.zero;
    }

    public float GetLookInputsHorizontal()
    {
        return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameHorizontal, GameConstants.k_AxisNameJoystickLookHorizontal);
    }

    public float GetLookInputsVertical()
    {
        return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameVertical, GameConstants.k_AxisNameJoystickLookVertical);
    }

    public bool GetJumpInputDown()
    {
        if (CanProcessInput())
        {
            return ControlFreak2.CF2Input.GetButtonDown(GameConstants.k_ButtonNameJump);
        }

        return false;
    }

    public bool GetJumpInputHeld()
    {
        if (CanProcessInput())
        {
            return ControlFreak2.CF2Input.GetButton(GameConstants.k_ButtonNameJump);
        }

        return false;
    }

    public bool GetFireInputDown()
    {
        return GetFireInputHeld() && !m_FireInputWasHeld;
    }

    public bool GetFireInputReleased()
    {
        return !GetFireInputHeld() && m_FireInputWasHeld;
    }

    public bool GetFireInputHeld()
    {
        if (CanProcessInput())
        {
            bool isGamepad = ControlFreak2.CF2Input.GetAxis(GameConstants.k_ButtonNameGamepadFire) != 0f;
            if (isGamepad)
            {
                return ControlFreak2.CF2Input.GetAxis(GameConstants.k_ButtonNameGamepadFire) >= triggerAxisThreshold;
            }
            else
            {
                return ControlFreak2.CF2Input.GetButton(GameConstants.k_ButtonNameFire);
            }
        }

        return false;
    }

    public bool GetAimInputHeld()
    {
        if (CanProcessInput())
        {
            bool isGamepad = ControlFreak2.CF2Input.GetAxis(GameConstants.k_ButtonNameGamepadAim) != 0f;
            bool i = isGamepad ? (ControlFreak2.CF2Input.GetAxis(GameConstants.k_ButtonNameGamepadAim) > 0f) : ControlFreak2.CF2Input.GetButton(GameConstants.k_ButtonNameAim);
            return i;
        }

        return false;
    }

    public bool GetSprintInputHeld()
    {
        if (CanProcessInput())
        {
            return ControlFreak2.CF2Input.GetButton(GameConstants.k_ButtonNameSprint);
        }

        return false;
    }

    public bool GetCrouchInputDown()
    {
        if (CanProcessInput())
        {
            return ControlFreak2.CF2Input.GetButtonDown(GameConstants.k_ButtonNameCrouch);
        }

        return false;
    }

    public bool GetCrouchInputReleased()
    {
        if (CanProcessInput())
        {
            return ControlFreak2.CF2Input.GetButtonUp(GameConstants.k_ButtonNameCrouch);
        }

        return false;
    }

    public int GetSwitchWeaponInput()
    {
        if (CanProcessInput())
        {

            bool isGamepad = ControlFreak2.CF2Input.GetAxis(GameConstants.k_ButtonNameGamepadSwitchWeapon) != 0f;
            string axisName = isGamepad ? GameConstants.k_ButtonNameGamepadSwitchWeapon : GameConstants.k_ButtonNameSwitchWeapon;

            if (ControlFreak2.CF2Input.GetAxis(axisName) > 0f)
                return -1;
            else if (ControlFreak2.CF2Input.GetAxis(axisName) < 0f)
                return 1;
            else if (ControlFreak2.CF2Input.GetAxis(GameConstants.k_ButtonNameNextWeapon) > 0f)
                return -1;
            else if (ControlFreak2.CF2Input.GetAxis(GameConstants.k_ButtonNameNextWeapon) < 0f)
                return 1;
        }

        return 0;
    }

    public int GetSelectWeaponInput()
    {
        if (CanProcessInput())
        {
            if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Alpha1))
                return 1;
            else if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Alpha2))
                return 2;
            else if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Alpha3))
                return 3;
            else if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Alpha4))
                return 4;
            else if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Alpha5))
                return 5;
            else if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Alpha6))
                return 6;
            else
                return 0;
        }

        return 0;
    }

    float GetMouseOrStickLookAxis(string mouseInputName, string stickInputName)
    {
        if (CanProcessInput())
        {
            // Check if this look input is coming from the mouse
            bool isGamepad = ControlFreak2.CF2Input.GetAxis(stickInputName) != 0f;
            float i = isGamepad ? ControlFreak2.CF2Input.GetAxis(stickInputName) : ControlFreak2.CF2Input.GetAxisRaw(mouseInputName);

            // handle inverting vertical input
            if (invertYAxis)
                i *= -1f;

            // apply sensitivity multiplier
            i *= lookSensitivity;

            if (isGamepad)
            {
                // since mouse input is already deltaTime-dependant, only scale input with frame time if it's coming from sticks
                i *= Time.deltaTime;
            }
            else
            {
                // reduce mouse input amount to be equivalent to stick movement
                i *= 0.01f;
#if UNITY_WEBGL
                // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
                i *= webglLookSensitivityMultiplier;
#endif
            }

            return i;
        }

        return 0f;
    }
}
