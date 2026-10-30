using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static readonly int HashMove = Animator.StringToHash("Move");

    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public LayerMask groundLayer;

    private Animator playerAnimator;
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;

    private Vector3 playerMove;


    private void Awake()
    {
        // 컴포넌트 연결
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = playerInput.MoveHorizontal;
        float Vertical = playerInput.MoveVertical;

        playerMove = new Vector3(horizontal, 0f, Vertical).normalized;

        // 회전 처리
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 targetPos = hit.point;
            targetPos.y = transform.position.y;


            Vector3 direction = targetPos - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        playerAnimator.SetFloat(HashMove, playerMove.magnitude);
    }

    private void FixedUpdate()
    {
        Vector3 delta = playerMove * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + delta);
    }
}
