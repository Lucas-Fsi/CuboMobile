using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private Vector2 moveInput;

    [Header("Particulas")]
    public ParticleSystem particulas;

    [Header("Cameras")]
    private CinemachineImpulseSource _impulseSource;



    private bool isGrounded = true;

    public float money = 0;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _impulseSource =GetComponent<CinemachineImpulseSource>();

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance == null) return;
        Movimentacao();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    private void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

        }
    }

    private void Movimentacao()
    {
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            Vector3 moveDirection = new Vector3
                (moveInput.x, 0, moveInput.y) * moveSpeed;
            rb.linearVelocity = new Vector3
                (moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(particulas, transform.position, Quaternion.identity);
            GameManager.Instance.GameOver();
            Destroy(gameObject);
            _impulseSource.GenerateImpulse();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            money += 10;
            Destroy(other.gameObject);
        }
    }
}

