using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour{
    [SerializeField] Image barImage;
    [SerializeField] Sprite[] barImages;

    public void UpdateBar(int currentAmount) {
        if(currentAmount - 2 > 0)
            barImage.sprite = barImages[currentAmount - 2];
        else
            barImage.sprite = barImages[0];
    }

}
