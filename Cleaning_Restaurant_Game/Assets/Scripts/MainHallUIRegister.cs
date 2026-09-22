using UnityEngine;
using UnityEngine.UI;

public class MainHallUIRegister : MonoBehaviour
{
    public Image clueDisplay;
    public Sprite[] clueSprites;
    public Image cleanBarFill;
    public Image endingImageUI;
    public CanvasGroup endingCanvasGroup;

    void Start()
    {
        GameManager.Instance.RegisterMainHallUI(clueDisplay, clueSprites, cleanBarFill, endingImageUI, endingCanvasGroup);
    }
}