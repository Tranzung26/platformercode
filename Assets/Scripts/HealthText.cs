using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthText : MonoBehaviour
{
    float _timeElapsed = 0f;
    Color _startColor;
    RectTransform _textTransform;
    TextMeshProUGUI _textMeshPro;

    [Tooltip("Determines the movement, in pixels/s, of this object.")]
    public Vector3 MoveSpeed = new(0, 75, 0);

    [Tooltip("Determines the time, in seconds, untill the text has fully faded out.")]
    public float FadeTime = 1f;

    private void Awake()
    {
        Debug.Assert(FadeTime >= 0, "'FadeTime' must be positive!");
        _textTransform = GetComponent<RectTransform>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startColor = _textMeshPro.color;
    }

    private void Update()
    {
        _textTransform.position += MoveSpeed * Time.deltaTime;
        _timeElapsed += Time.deltaTime;

        // TODO: Code copy from FadeRemoveBehavior -> Extract to reusable code
        if (_timeElapsed < FadeTime)
        {
            var newAlpha = _startColor.a * (1 - (_timeElapsed / FadeTime));
            _textMeshPro.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
