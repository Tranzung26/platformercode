using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HealthPickup : MonoBehaviour
{
    AudioSource _audioSource;

    [Tooltip("The amount of health restored")]
    public int HealthRestored = 20;

    [Tooltip("The speed, in degrees/s, at which the pickup rotates")]
    public Vector3 SpinRotationSpeed = new Vector3(0, 180, 0);

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.eulerAngles += SpinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Damagable>(out var damagable))
        {
            bool wasHealed = damagable.Heal(HealthRestored);
            if(wasHealed)
            {
                AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position, _audioSource.volume);
                Destroy(gameObject);
            }
        }
    }
}
