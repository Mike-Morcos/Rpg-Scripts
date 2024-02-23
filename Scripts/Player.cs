using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 _moveDirection;
    Animator _animator;
    Vector2 _lastWalkDirection; 

    [SerializeField] float _moveSpeed;
    [SerializeField] GameInput gameInput;

    const string WALK_LEFT = "player_walk_left";
    const string WALK_UP = "player_walk_up";
    const string WALK_DOWN = "player_walk_down";
    const string WALK_RIGHT = "player_walk_left";

    const string IDLE_LEFT = "player_idle_left";
    const string IDLE_UP = "player_idle_up";
    const string IDLE_DOWN = "player_idle_down";
    const string IDLE_RIGHT = "player_idle_left";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetPlayerInput();

        MovePlayer();
        
        SetWalkAnimations(_moveDirection);
    }

    void GetPlayerInput()
    {
        _moveDirection = gameInput.GetMovementVectorNormalized();
    }

    void MovePlayer()
    {
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    void SetWalkAnimations(Vector2 moveDirection)
    {
        if (moveDirection.magnitude > 0)
        {
            if (moveDirection.x < 0)
            {
                _animator.Play(WALK_LEFT);
                _lastWalkDirection = Vector2.left;
            }

            if (moveDirection.x > 0)
            {
                _animator.Play(WALK_RIGHT);
                _lastWalkDirection = Vector2.right;
            }

            if (moveDirection.y > 0)
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
}
