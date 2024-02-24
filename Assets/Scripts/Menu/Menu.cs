using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject shop;

    [SerializeField]
    private GameObject levelsMenu;

    void Awake()
    {
        gameObject.SetActive(true);
        shop.SetActive(false);
        AddStaticInformation();
    }

    private void AddStaticInformation()
    {
        shop.GetComponent<Shop>().StaticWeapons();
    }

    public void OpenShop()
    {
        gameObject.SetActive(false);
        shop.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenLevelsMenu()
    {
        gameObject.SetActive(false);
        levelsMenu.SetActive(true);
    }
}
