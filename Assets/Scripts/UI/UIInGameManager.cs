using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class UIInGameManager : Singleton<UIInGameManager>
{

    public TextMeshProUGUI uiTextCoins;

    public TextMeshProUGUI uiTextRedCoins;

    public static void UpdateTextCoins(string s, string s2)
    {
       instance.uiTextCoins.text = s;
       instance.uiTextRedCoins.text = s2;
    }

    
}
