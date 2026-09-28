using UnityEngine;

public class BackToMenuButtonHandler : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.OnClickBackToMenu();
    }
}