using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode LeftMouseButton = KeyCode.Mouse0;
    private const KeyCode Space = KeyCode.Space;

    private bool _isJump;
    private bool _isShooting;

    private void Update()
    {
        if (Input.GetKeyDown(LeftMouseButton))
            _isShooting = true;

        if (Input.GetKeyDown(Space))
            _isJump = true;
    }

    public bool GetIsShooting() => GetBoolAsTrigger(ref _isShooting);
    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}