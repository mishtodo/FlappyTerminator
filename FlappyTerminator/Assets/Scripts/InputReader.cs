using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode LeftMouseButton = KeyCode.Mouse0;
    private const KeyCode RightMouseButton = KeyCode.Mouse1;
    private const KeyCode Space = KeyCode.Space;

    private bool _isShooting;
    private bool _isLasering;
    private bool _isJump;

    private void Update()
    {
        if (Input.GetKeyDown(LeftMouseButton))
            _isShooting = true;

        if (Input.GetKeyDown(RightMouseButton))
            _isLasering = true;

        if (Input.GetKeyDown(Space))
            _isJump = true;
    }

    public bool GetIsShooting() => GetBoolAsTrigger(ref _isShooting);
    public bool GetIsLasering() => GetBoolAsTrigger(ref _isLasering);
    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}