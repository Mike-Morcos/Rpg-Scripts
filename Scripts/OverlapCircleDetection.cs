using UnityEngine;

public class OverlapCircleDetection : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private float _interactionRadius = 1f;
    float _brightnessFactor = 1.2f;
    private CircleCollider2D _thisCircleCollider;
    private Color _originalColor; 

    private void Start()
    {
        _thisCircleCollider = GetComponent<CircleCollider2D>();

        if (_thisCircleCollider != null)
        {
            _thisCircleCollider.radius = _interactionRadius;
        }
        else
        {
            Debug.LogWarning("CircleCollider2D component not found on this GameObject.");
        }
    }
    public void DetectObject(Player player)
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactionRadius, _interactableLayerMask);
        if (collider != null)
        {
            Debug.Log(collider.gameObject.name);

            ClearCounter clearCounter;
            if (collider.gameObject.TryGetComponent<ClearCounter>(out clearCounter))
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
        // Check if the detected object is on the interactable layer mask
        if (((1 << other.gameObject.layer) & _interactableLayerMask) != 0)
        {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                _originalColor = renderer.material.color;
                Color brighterColor = _originalColor * _brightnessFactor;
                renderer.material.color = brighterColor;
            }
            else
            {
                Debug.LogWarning("Renderer component not found on detected object.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _interactableLayerMask) != 0)
        {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = _originalColor;
            }
            else
            {
                Debug.LogWarning("Renderer component not found on detected object.");
            }
        }
    }

}
