using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private GameObject _levelsMenu;

    void Awake()
    {
        ChangeActive(gameObject, _shop.gameObject, true);
        AddStaticInformation();
    }

    private void AddStaticInformation()
    {
        _shop.StaticWeapons();
    }

    public void OpenShop()
    {
        ChangeActive(gameObject, _shop.gameObject, false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenLevelsMenu()
    {
        ChangeActive(gameObject, _levelsMenu, false);
    }

    private void ChangeActive(GameObject canvasOne, GameObject canvasTwo, bool isActive)
    {
        canvasOne.SetActive(isActive);
        canvasTwo.SetActive(!isActive);
    }
}
