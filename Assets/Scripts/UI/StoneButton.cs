using UnityEngine;
using UnityEngine.UI;

public class StoneButton : MonoBehaviour{
    [SerializeField] private Button _button;

    [SerializeField] StoneType _type;

    //public Button Button => _button;

    public void SetAction(UnityEngine.Events.UnityAction<StoneType> action) {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => action(_type));
    }

    private void OnDestroy() {
        _button.onClick.RemoveAllListeners();
    }
}
