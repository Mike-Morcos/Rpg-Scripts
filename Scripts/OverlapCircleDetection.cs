using UnityEngine;

public class OverlapCircleDetection : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private float _interactionRadius = 1f;
    [SerializeField] private float _brightnessFactor = 1.2f;
    private CircleCollider2D _circleCollider;
    private Color _originalColor;

    private void Start()
    {
        SetupCircleCollider();
    }

    private void SetupCircleCollider()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        if (_circleCollider)
        {
            _circleCollider.radius = _interactionRadius;
        }
        else
        {
            Debug.LogWarning("CircleCollider2D component not found on this GameObject.", this);
        }
    }

    public void DetectObject(Player player)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactionRadius, _interactableLayerMask);
        if (collider)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact(player);
            }
            else
            {
                Debug.Log("ClearCounter component not found on detected object.");
            }
        }
        else
        {
            Debug.Log("No object detected within the overlap circle.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _interactableLayerMask) != 0 && other.TryGetComponent<Renderer>(out var renderer))
        {
            _originalColor = renderer.material.color;
            renderer.material.color *= _brightnessFactor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _interactableLayerMask) != 0 && other.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material.color = _originalColor;
        }
    }
}
