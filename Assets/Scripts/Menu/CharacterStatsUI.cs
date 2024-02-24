using UnityEngine;
using UnityEngine.UI;

public class CharacterStatUI : MonoBehaviour
{
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _armorText;
    [SerializeField] private Text _bulletsText;

    public Text HpText => _hpText;
    public Text ArmorText => _armorText;
    public Text BulletsText => _bulletsText;

    public void ChangeText(Text text, string content)
    {
        text.text = content;
    }
}
