using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour{
    [SerializeField] Image _barImage;
    [SerializeField] Sprite[] _barImages;


    private void Awake()
    {
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        _barImage.sprite = _barImages[0];
    }

    public void UpdateBar(int currentAmount) {
        if(currentAmount - 2 > 0)
            _barImage.sprite = _barImages[currentAmount - 2];
        else
            _barImage.sprite = _barImages[0];
    }

}
