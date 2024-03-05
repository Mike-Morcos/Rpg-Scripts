
using UnityEngine;

public class Player : MonoBehaviour, IHeldObjectParent
{
    // Fields
    private Vector2 _moveDirection;
    private Vector2 _lastWalkDirection;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameInput _gameInput;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private OverlapCircleDetection _overlapCircleDetection;
    [SerializeField] private Transform _handPosition;
    private HeldObject _heldObject;

    // Animation strings
    private const string WALK_LEFT = "player_walk_left";
    private const string WALK_RIGHT = "player_walk_right";
    private const string WALK_UP = "player_walk_up";
    private const string WALK_DOWN = "player_walk_down";
    private const string IDLE_LEFT = "player_idle_left";
    private const string IDLE_RIGHT = "player_idle_right";
    private const string IDLE_UP = "player_idle_up";
    private const string IDLE_DOWN = "player_idle_down";

    private void Start()
    {
        InitializeComponents();
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void InitializeComponents()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetPlayerInput();
        SetAnimationState();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region Input and Movement
    private void GetPlayerInput()
    {
        _moveDirection = _gameInput.GetMovementVectorNormalized();
    }

    private void MovePlayer()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed * Time.fixedDeltaTime;
    }
    #endregion

    #region Animation Handling
    private void SetAnimationState()
    {
        string animationName = _moveDirection.magnitude > 0 ? GetWalkAnimation() : GetIdleAnimation();
        _animator.Play(animationName);
        UpdateLastWalkDirection();
    }

    private string GetWalkAnimation()
    {
        if (_moveDirection.x < 0) return WALK_LEFT;
        if (_moveDirection.x > 0) return WALK_RIGHT;
        if (_moveDirection.y > 0) return WALK_UP;
        if (_moveDirection.y < 0) return WALK_DOWN;
        return GetIdleAnimation();
    }

    private string GetIdleAnimation()
    {
        if (_lastWalkDirection == Vector2.left) return IDLE_LEFT;
        if (_lastWalkDirection == Vector2.right) return IDLE_RIGHT;
        if (_lastWalkDirection == Vector2.up) return IDLE_UP;
        if (_lastWalkDirection == Vector2.down) return IDLE_DOWN;
        return IDLE_DOWN; // Default idle state
    }

    private void UpdateLastWalkDirection()
    {
        if (_moveDirection != Vector2.zero)
        {
            _lastWalkDirection = _moveDirection;
            _spriteRenderer.flipX = _moveDirection.x > 0;
        }
    }
    #endregion

    #region Interaction
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        _overlapCircleDetection.DetectObject(this);
    }
    #endregion

    #region IHeldObjectParent Implementation
    public Transform GetHeldObjectTransform() => _handPosition;

    public void SetHeldObject(HeldObject heldObject)
    {
        _heldObject = heldObject;
    }

    public HeldObject GetHeldObject() => _heldObject;

    public void ClearHeldObject() => _heldObject = null;

    public bool HasHeldObject() => _heldObject != null;
    #endregion
}
