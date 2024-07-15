using Characters.Slime;
using Map;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed = 0.2f;


    [SerializeField] public float health, maxHealth = 120f;

    [SerializeField] public FloatingHealthBar healthBar;
    [SerializeField] private float timeToReturn = 60f;
    private float timer = 0f;

    [SerializeField] private LayerMask obstacleLayer; // Layer mask to specify the obstacle layers
    private Animator _animator;
    private Rigidbody2D _rb;
    private SlimePool _slimePool;
    private SpriteRenderer _spriteRenderer;

    private Vector3 initialPosition; // Store the initial position of the Slime
    private bool isDefeated;
    private Transform playerTransform;
    private bool isDetectedPlayer;
    private GamePlayScript _gamePlay;


    public float Heatth
    {
        set
        {
            health = value;

            if (health <= 0) Defeated();
        }
        get => health;
    }


    // Start is called before the first frame update
    private void Start()
    {
        _slimePool = FindObjectOfType<SlimePool>();
        playerTransform = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
        initialPosition = transform.position; // Store the initial position
        _gamePlay = FindObjectOfType<GamePlayScript>();
    }

    void OnEnable()
    {
        isDefeated = false;
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (playerTransform != null) // Check if player exists
        {
            // Calculate direction towards player
            Vector2 direction = playerTransform.position - transform.position;
            direction.Normalize(); // Normalize the direction vector to have a magnitude of 1


            // Check if there is an obstacle between slime and player
            var hit = Physics2D.Raycast(transform.position, direction,
                Vector2.Distance(transform.position, playerTransform.position), obstacleLayer);
            if (hit.collider != null)
            {
                // Obstacle detected, go to initial position
                Vector2 returnDirection = initialPosition - transform.position;
                returnDirection.Normalize();
                _rb.MovePosition(_rb.position + returnDirection * (moveSpeed * Time.fixedDeltaTime));
                _animator.SetBool("IsMoving", returnDirection.magnitude > 0);
                timer += Time.fixedDeltaTime; // Increment the timer by the elapsed time since last FixedUpdate call
                if (timer >= timeToReturn && isDetectedPlayer)
                {
                    RemoveEnemy(); // Call the method to return the slime to the pool
                }
            }
            else
            {
                // No obstacle, move towards player
                // Move the enemy towards the player
                _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
                // Flip the enemy sprite based on the direction
                _spriteRenderer.flipX = direction.x < 0;
                _animator.SetBool("IsMoving", direction.magnitude > 0);
                timer = 0;
                isDetectedPlayer = true;
            }
        }
    }

    public void Defeated()
    {
        if (!isDefeated)
        {
            isDefeated = true;
            _animator.SetTrigger("Defeated");
            _gamePlay.AddEnemyDefeated();
        }
    }

    public void RemoveEnemy()
    {
        _slimePool.ReturnSlime(gameObject);
    }

    public void TakeDamage(float damgeAmount)
    {
        health -= damgeAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) Defeated();
    }
}