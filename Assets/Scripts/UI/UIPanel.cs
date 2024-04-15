using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class UIPanel : MonoBehaviour
{
    [Inject]
    MainGameManager _uiManager;

    [SerializeField] private SummonButton _button;
    [SerializeField] private StoneButton[] _buttons;
    [SerializeField] private TextMeshProUGUI _animalText;
    [SerializeField] private TextMeshProUGUI _moneyText;

    [SerializeField] private string[] _repeatTexts;
    [SerializeField] private string[] _monsterTexts;

    void Awake()
    {
        foreach (var button in _buttons)
        {
            button.SetAction(SetStone);
        }
        EventsBus.Subscribe<OnStartSummon>(this, OnStartSummon);
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    private void OnStartSummon(OnStartSummon data)
    {
        foreach (var button in _buttons)
        {
            button.HideButton();
        }
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        _animalText.gameObject.SetActive(false);
        _uiManager.SetStone(new StoneInfo(StoneType.None));
        foreach (var button in _buttons)
        {
            button.ShowButton();
        }
    }

    private void SetStone(StoneType type)
    {
        StoneInfo stone = new StoneInfo(type);
        _uiManager.SetStone(stone);
    }

    public void OnChangeMoney(int money)
    {
        _moneyText.text = money.ToString("D5");
        DOTween.Kill(_moneyText.transform);
        _moneyText.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f).SetLoops(1);
    }

    public void SetAnimalText(string animalName)
    {
        if (animalName == "monster")
            animalName = _monsterTexts[new System.Random().Next(_monsterTexts.Length)];
        if (animalName == "repeat")
            animalName = _repeatTexts[new System.Random().Next(_repeatTexts.Length)];

        DOTween.Kill(_animalText.transform);
        _animalText.gameObject.SetActive(true);
        _animalText.transform.localScale = Vector3.zero;
        _animalText.text = animalName;
        _animalText.transform.DOScale(Vector3.one, 0.3f).SetDelay(1f);

    }

    private void OnDestroy()
    {
        DOTween.Kill(_animalText.transform);
        DOTween.Kill(_moneyText.transform);
    }
}
