using UnityEngine;
using TMPro;

public class HpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private DefeatChecker _defeatChecker;
    [SerializeField] private string _description;

    private void Start()
    {
        ChangeHpText(_defeatChecker.CurrentHp);
    }

    private void OnEnable()
    {
        _defeatChecker.HpChanged.AddListener(OnHpChanged);
    }

    private void OnDisable()
    {
        _defeatChecker.HpChanged.RemoveListener(OnHpChanged);
    }

    private void OnHpChanged(DefeatChecker _, int prevValue, int newValue)
    {
        ChangeHpText(newValue);
    }

    private void ChangeHpText(int newValue)
    {
        _hpText.text = $"{_description}: {newValue}";
    }
}