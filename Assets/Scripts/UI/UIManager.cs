using UnityEngine;

public class UIManager : MonoBehaviour
{
    private StoneInfo _currentStone;

    public void SetStone(StoneInfo newStone) => _currentStone = newStone;
    

}
