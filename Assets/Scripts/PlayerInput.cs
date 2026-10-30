using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string MoveAxisVertical = "Vertical";
    public static readonly string MoveAxisHorizontal = "Horizontal";
    public static readonly string FireAxis = "Fire1";

    public float MoveVertical { get; private set; }
    public float MoveHorizontal { get; private set; }
    public float Rotate { get; private set; }
    public bool Fire { get; private set; }

    private void Update()
    {
        MoveVertical = Input.GetAxisRaw(MoveAxisVertical);
        MoveHorizontal = Input.GetAxisRaw(MoveAxisHorizontal);
        Fire = Input.GetButton(FireAxis);

    }
}
