using UnityEngine;
using Zenject;

public class BottomPanel : MonoBehaviour
{

    [Inject]
    UIManager _uiManager;

    [SerializeField] private StoneButton _waterButton;
    [SerializeField] private StoneButton[] _buttons;


    void Start()
    {
        _waterButton.SetAction(SetStone);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetStone(StoneType type) {
        StoneInfo stone = new StoneInfo(type);
        _uiManager.SetStone(stone);
    }
}
