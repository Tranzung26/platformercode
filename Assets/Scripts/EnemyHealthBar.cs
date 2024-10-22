using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Damagable _damagable;

    public Slider HealthBarSlider;

    private void Awake()
    {
        // Lấy thành phần Damagable từ đối tượng này (tức là quái vật)
        _damagable = GetComponentInParent<Damagable>();
        Debug.Assert(_damagable != null, "The parent object must have a 'Damagable' component!");
    }

    void Start()
    {
        HealthBarSlider.value = CalculateSliderPercentage();
    }

    private void OnEnable()
    {
        _damagable.HealthChanged.AddListener(OnEnemyHealthChanged);
    }

    private void OnDisable()
    {
        _damagable.HealthChanged.RemoveListener(OnEnemyHealthChanged);
    }

    private float CalculateSliderPercentage() => (float)_damagable.Health / _damagable.MaxHealth;

    private void OnEnemyHealthChanged()
    {
        HealthBarSlider.value = CalculateSliderPercentage();
    }
}
