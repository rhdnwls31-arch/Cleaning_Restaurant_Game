using UnityEngine;
using UnityEngine.UI;

public class StaffRoomUIRegister : MonoBehaviour
{
    public Image clueDisplay;
    public Sprite[] clueSprites;
    public Image cleanBarFill;

    void Start()
    {
        GameManager.Instance.RegisterStaffRoomUI(clueDisplay, clueSprites, cleanBarFill);
    }
}