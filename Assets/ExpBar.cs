using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    private PlayerController _playerController;

    public TMP_Text ExperienceBarText;
    public Slider ExperienceBarSlider;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(player != null, "Phải có một đối tượng được gắn thẻ 'Player'!");
        _playerController = player?.GetComponent<PlayerController>();
        Debug.Assert(_playerController != null, "Đối tượng gắn thẻ 'Player' phải có component 'PlayerController'!");
    }

    void Start()
    {
        if (_playerController != null)
        {
            UpdateExperienceBar();
        }
    }

    private void OnEnable()
    {
        if (_playerController != null)
        {
            _playerController.OnExperienceGained += UpdateExperienceBar;
        }
    }

    private void OnDisable()
    {
        if (_playerController != null)
        {
            _playerController.OnExperienceGained -= UpdateExperienceBar;
        }
    }

    private float CalculateSliderPercentage()
    {
        return (float)_playerController.Experience / _playerController.XPThreshold;
    }

    private void UpdateExperienceBar()
    {
        if (_playerController != null)
        {
            ExperienceBarSlider.value = CalculateSliderPercentage();
            ExperienceBarText.text = $"XP {_playerController.Experience} / {_playerController.XPThreshold}";
        }
    }
}
