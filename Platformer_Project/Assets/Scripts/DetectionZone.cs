using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    // TODO: Given item based membership checking, HashSet<Collider2D>() may be the better abstraction?
    // This would ensure that the same thing cannot be detected multiple times.
    // Alternatively, if we only ever collide with 1 thing, then perhaps we only need a field instead?
    public List<Collider2D> DetectedColliders = new List<Collider2D>(); // TODO: This probably should not be a public field
    Collider2D _col; // TODO: Can this component be removed? It's not being used?

    public UnityEvent NoCollisionsRemain;

    void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectedColliders.Remove(collision);

        if(DetectedColliders.Count == 0)
        {
            NoCollisionsRemain?.Invoke();
        }
    }
}
