using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour, IOpenMenu
{
    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private Text amountMoney;

    private void Awake()
    {
        amountMoney.text = SaveParameters.money.ToString();
        StaticWeapons();
        SearchEqiupWeapon();
    }

    public void StaticWeapons()
    {
        if (SaveParameters.weaponsBuy == null)
        {
            SaveParameters.levelCount = SceneManager.sceneCountInBuildSettings;
            SaveParameters.levelComplete = new bool[SaveParameters.levelCount];
            SaveParameters.levelPoints = new int[SaveParameters.levelCount];
            SaveParameters.levelStars = new int[SaveParameters.levelCount];
            SaveParameters.levelActive = 1;

            SaveParameters.money = 0;
            amountMoney.text = SaveParameters.money.ToString();
            SaveParameters.weaponsBuy = new GameObject[weapons.Length];
            SaveParameters.weaponEquip = 0;
            SaveParameters.weaponsBuy[0] = weapons[0];
        }
    }

    public void Equip(int weaponNum)
    {
        Text[] isBuy = gameObject.GetComponentsInChildren<Text>();

        if (Char.IsDigit(isBuy[weaponNum].text[0]))
        {
            BuyWeapon(isBuy[weaponNum], weapons[weaponNum], weaponNum);
        }
        else if(isBuy[weaponNum].text == "Экипировать")
        {
            SaveParameters.weaponEquip = weaponNum;
            SearchEqiupWeapon();
        }
    }

    private void SearchEqiupWeapon()
    {
        Text[] isBuy = gameObject.GetComponentsInChildren<Text>();

        foreach(GameObject weapon in SaveParameters.weaponsBuy)
        {
            foreach(Button butWeapon in gameObject.GetComponentsInChildren<Button>())
            {
                if(weapon != null)
                {
                    if (weapon.name.ToLower() == butWeapon.name.ToLower())
                    {
                        butWeapon.GetComponentInChildren<Text>().text = "Экипировать";
                        butWeapon.GetComponent<Image>().material = null;
                        butWeapon.GetComponentInChildren<Text>().fontSize = 52;
                    }
                }
            }
        }

        isBuy[SaveParameters.weaponEquip].GetComponentInParent<Button>().GetComponent<Image>().material = null;
        isBuy[SaveParameters.weaponEquip].fontSize = 52;
        isBuy[SaveParameters.weaponEquip].text = "Экипирован";
    }

    private void BuyWeapon(Text isBuy, GameObject weapon, int weaponNum)
    {
        int price = int.Parse(isBuy.text.Trim().Replace(" ", string.Empty));
        if (SaveParameters.money >= price)
        {
            isBuy.fontSize = 52;
            isBuy.text = "Экипировать";
            gameObject.GetComponentsInChildren<Button>()[weaponNum].GetComponent<Image>().material = null;
            SaveParameters.weaponsBuy[weaponNum] = weapon;
            SaveParameters.money -= price;
            amountMoney.text = SaveParameters.money.ToString();
        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
