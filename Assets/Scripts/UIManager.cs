using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _winPopup;
    [SerializeField] private GameObject _losePopup;

    private void OnEnable()
    {
        _gameManager.onWin += ActivateWin;
        _gameManager.onLose += ActivateLose;
    }

    private void OnDisable()
    {
        _gameManager.onWin -= ActivateWin;
        _gameManager.onLose -= ActivateLose;
    }

    private void ActivateWin()
    {
        _winPopup.SetActive(true);
    }

    private void ActivateLose()
    {
        _losePopup.SetActive(true);
    }
}
