using System.Collections;
using System.Collections.Generic;
using UI.Scripts.Sound;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Defeat = Animator.StringToHash("Defeat");
        [Range(0.1f, 1.5f)] public float moveSpeed = 1f;
        [Range(0f, 0.1f)] public float collisionOffset = 0.05f;
        public ContactFilter2D movementFilter;
        public int maxHealth = 100;
        public int currentHealth;
        private readonly List<RaycastHit2D> _castCollisions = new();
        private Animator _animator;
        private bool _canMove = true;
        private Vector2 _inputMovement;
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private HealthBarController healthBar;
        public float pushBackForce = 1f;

        private GameObject _swordHitBox;

        public GameplaySound manageSound;
        // Start is called before the first frame update 
        private void Start()
        {
            
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _swordHitBox = transform.Find("SwordHitBox").gameObject;
            healthBar = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
            healthBar.SetMaxHealth(maxHealth);
            currentHealth = maxHealth;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (_canMove && _inputMovement != Vector2.zero)
            {
                var success = TryMove(_inputMovement);

                if (!success) success = TryMove(new Vector2(_inputMovement.x, 0));

                if (!success) success = TryMove(new Vector2(0, _inputMovement.y));





                _animator.SetBool(IsMoving, success);
                _spriteRenderer.flipX = _inputMovement.x < 0;
                _animator.SetFloat(Horizontal, _inputMovement.x);
                _animator.SetFloat(Vertical, _inputMovement.y);
            }
            else
            {
                _animator.SetBool(IsMoving, false);
            }
        }

        private void OnMove(InputValue value)
        {
            _inputMovement = value.Get<Vector2>();
        }

        private void OnFire()
        {
            manageSound.PlaySFX(manageSound.SwordSound);
            _animator.SetTrigger(Attack);
            Debug.Log(manageSound.SwordSound);
            
        }

        private bool TryMove(Vector2 direction)
        {
            var count = _rb.Cast(
                direction,
                movementFilter,
                _castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count != 0) return false;
            _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
            return true;
        }

        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                TakeDamage(10);
            }
        }

        void Defeated()
        {
            _animator.SetTrigger(Defeat);
        }

        public void EndGame()
        {
            Time.timeScale = 0;
        }

        private void LockMovement()
        {
            _swordHitBox.SetActive(true);
            _canMove = false;
        }

        private void UnlockMovement()
        {
            _swordHitBox.SetActive(false);
            _canMove = true;
        }

        private void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            // _rb.AddForce(-_inputMovement * pushBackForce, ForceMode2D.Impulse); not work
            if (currentHealth <= 0)
            {
                Defeated();
            }
        }
    }
}