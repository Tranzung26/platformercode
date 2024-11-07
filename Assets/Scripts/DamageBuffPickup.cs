using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Collider2D))]
public class DamageBuffPickup : MonoBehaviour
{
    AudioSource _audioSource;
    Collider2D _pickupCollider;

    public int DamageBoost = 10;
    public float BoostDuration = 10f;
    public Vector3 SpinRotationSpeed = new Vector3(0, 180, 0);
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _pickupCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += SpinRotationSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerAttacks = collision.GetComponentsInChildren<Attack>();

        if (playerAttacks.Length > 0)
        {
            Debug.Log("Collision with player detected");

            foreach (var playerDamage in playerAttacks)
            {
                playerDamage.BoostDamage(DamageBoost, BoostDuration);
            }

            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position, _audioSource.volume);

            Debug.Log("Destroying pickup object");

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No Attack components found on collision object");
        }
    }
}
