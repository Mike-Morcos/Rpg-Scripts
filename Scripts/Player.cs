using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFoodObjectParent
{
    Vector2 _moveDirection;
    Vector2 _lastWalkDirection; 

    [SerializeField] float _moveSpeed;
    [SerializeField] GameInput _gameInput;

    const string WALK_LEFT = "player_walk_left";
    const string WALK_UP = "player_walk_up";
    const string WALK_DOWN = "player_walk_down";
    const string WALK_RIGHT = "player_walk_left";

    const string IDLE_LEFT = "player_idle_left";
    const string IDLE_UP = "player_idle_up";
    const string IDLE_DOWN = "player_idle_down";
    const string IDLE_RIGHT = "player_idle_left";

    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;

    [SerializeField] OverlapCircleDetection _overlapCircleDetection;


    private FoodObject _foodObject;
    [SerializeField] private Transform _handPosition; 

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        _overlapCircleDetection.DetectObject(this);
    }

    private void Update()
    {
        GetPlayerInput();

        
        SetWalkAnimations(_moveDirection);
    }

    private void FixedUpdate()
    {

        MovePlayer();
    }

    void GetPlayerInput()
    {
        _moveDirection = _gameInput.GetMovementVectorNormalized();
    }

    void MovePlayer()
    {
        _rigidbody.velocity = _moveDirection * _moveSpeed* Time.fixedDeltaTime;
        //transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    void SetWalkAnimations(Vector2 moveDirection)
    {
        if (moveDirection.magnitude > 0)
        {
            if (moveDirection.x < 0 && moveDirection.y == 0)
            {
                _spriteRenderer.flipX = false;
                _animator.Play(WALK_LEFT);
                _lastWalkDirection = Vector2.left;
            }

            if (moveDirection.x > 0 && moveDirection.y == 0)
            {
                _spriteRenderer.flipX = true;
                _animator.Play(WALK_RIGHT);
                _lastWalkDirection = Vector2.right;
            }

            if (moveDirection.y > 0 )
            {
                _animator.Play(WALK_UP);
                _lastWalkDirection = Vector2.up;
            }

            if (moveDirection.y < 0)
            {
                _animator.Play(WALK_DOWN);
                _lastWalkDirection = Vector2.down;
            }
        }
        else
        {
            // Player is not moving, set appropriate idle animation
            SetIdleAnimation(_lastWalkDirection);
        }
    }

    void SetIdleAnimation(Vector2 walkDirection)
    {
        // Determine the last non-zero movement direction and set the appropriate idle animation
        if (walkDirection.x < 0)
        {
            _animator.Play(IDLE_LEFT);
        }
        else if (walkDirection.x > 0)
        {
            _animator.Play(IDLE_RIGHT);
        }
        else if (walkDirection.y > 0)
        {
            _animator.Play(IDLE_UP);
        }
        else if (walkDirection.y < 0)
        {
            _animator.Play(IDLE_DOWN);
        }
        else
        {
            // If no movement, set a default idle animation (e.g., IDLE_DOWN)
            _animator.Play(IDLE_DOWN);
        }
    }

    // Getter method for the food object's transform, though the method name might suggest returning the FoodObject's transform instead of the countertop's.
    public Transform GetFoodObjectTransform()
    {
        return _handPosition;
    }

    // Setter method to set the food object. It's clear and straightforward.
    public void SetFoodObject(FoodObject foodObject)
    {
        _foodObject = foodObject;
    }

    // Getter method for the food object. It provides a good way to access the private field _foodObject from other classes.
    public FoodObject GetFoodObject()
    {
        return _foodObject;
    }

    // Method to clear the reference to the food object. Simple and effective for resetting the state.
    public void ClearFoodObject()
    {
        _foodObject = null;
    }

    public bool HasFoodObject() { return _foodObject != null; }

}
