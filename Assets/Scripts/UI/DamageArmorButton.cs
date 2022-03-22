using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageArmorButton : MonoBehaviour
{
    public InputField inputField;
    public Dropdown dropdown;
    public Unit unit;
    public Unit target;
    // Start is called before the first frame update

    public void Modify()
    {
        int number = 0;
        if (!int.TryParse(inputField.text, out number))
        {
            TipManager.ShowTip("�����쳣");
            return;
        }
        else
        {
            switch (dropdown.options[dropdown.value].text)
            {
                case "�˺�":
                    target.TakeDamage(number);
                    break;
                case "����":
                    unit.armor += number;
                    break;
                default:
                    break;
            }
        }
    }
}
