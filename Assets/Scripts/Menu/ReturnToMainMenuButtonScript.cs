using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnToMainMenuButtonScript : MonoBehaviour
{
    private Button _backButton;

    public void Initialize(MainMenu mainMenu)
    {
        Canvas parentCanvas = GetParentCanvas();
        _backButton = GetComponent<Button>();
        
        SetBackButtonLogic(parentCanvas, mainMenu);
    }

    private void SetBackButtonLogic(Canvas currentMenu, MainMenu mainMenu)
    {
        _backButton.onClick.AddListener(() =>
        {
            mainMenu.gameObject.SetActive(true);
            currentMenu.gameObject.SetActive(false);
        });
    }

    private Canvas GetParentCanvas()
    {
        Canvas menuCanvas = gameObject.GetComponentInParent<Canvas>();

        if (menuCanvas == null)
        {
            Debug.LogError("Back Button isn't a direct child of the canvas");
        }

        return menuCanvas;
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveAllListeners();
    }
}
