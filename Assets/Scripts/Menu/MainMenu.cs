using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Menu
{
    [SerializeField] private Canvas _menu;
    [SerializeField] private Button _redirectionButton;

    public Canvas MenuCanvas { get { return _menu; }}
    public Button RedirectionButton { get { return _redirectionButton; }}

    public Menu(Canvas menu, Button redirectionButton)
    {
        _menu = menu;
        _redirectionButton = redirectionButton;
    }
}

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private Menu[] _menus;

    private void Awake()
    {
        ActionPerformer.PerformActionOnObjects<Menu>(_menus, menu =>
        {
            AttachRedirectToButton(menu.MenuCanvas, menu.RedirectionButton);
        });
    }

    private void Start()
    {
        InitializeBackButtons();
    }

    private void InitializeBackButtons()
    {
        ReturnToMainMenuButtonScript[] backButtons = FindObjectsByType<ReturnToMainMenuButtonScript>(FindObjectsSortMode.None);

        ActionPerformer.PerformActionOnObjects<ReturnToMainMenuButtonScript>(backButtons, buttonScript => buttonScript.Initialize(this));
    }

    private void AttachRedirectToButton(Canvas menu, Button redirectButton)
    {
        redirectButton.onClick.AddListener(() =>
        {
            menu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }

    private void OnDestroy()
    {
        ActionPerformer.PerformActionOnObjects<Menu>(_menus, menu =>
        {
            menu.RedirectionButton.onClick.RemoveAllListeners();
        });
    }
}
