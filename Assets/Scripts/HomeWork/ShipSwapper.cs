using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShipSwapper : MonoBehaviour
{
    [SerializeField] private Button _selectNextShip;
    [SerializeField] private Button _selectPreviousShip;
    [SerializeField] private GameObject[] _ships;

    private int _currentShipIndex = 0;

    public GameObject CurrentShip => _ships[_currentShipIndex];

    private void Awake()
    {
        if (_ships.Length <= 0)
        {
            Debug.LogError("SHIPS LIST IS EMPTY!");
        }

        BindButtonsToActions();
        HideAllShips();
        SelectNextShip();
    }

    private void BindButtonsToActions()
    {
        _selectNextShip.onClick.AddListener(SelectNextShip);
        _selectPreviousShip.onClick.AddListener(SelectPreviousShip);
    }

    private void HideAllShips()
    {
        foreach (GameObject ship in _ships)
        {
            ship.SetActive(false);
        }
    }

    private void SelectNextShip()
    {
        _ships[_currentShipIndex].gameObject.SetActive(false);

        _currentShipIndex++;

        if (_currentShipIndex >= _ships.Length)
        {
            _currentShipIndex = 0;
        }

        _ships[_currentShipIndex].gameObject.SetActive(true);
    }

    private void SelectPreviousShip()
    {
        _ships[_currentShipIndex].gameObject.SetActive(false);

        _currentShipIndex--;

        if (_currentShipIndex < 0)
        {
            _currentShipIndex = _ships.Length - 1;
        }

        _ships[_currentShipIndex].gameObject.SetActive(true);
    }
}
