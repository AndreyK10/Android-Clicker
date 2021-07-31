using UnityEngine;
using TMPro;


public class UIControllerScript : MonoBehaviour
{
    public TextMeshProUGUI playerResourcesText, buyStationText, upgradeToolText, stationInfoText, toolInfoText;

    void Update()
    {
        playerResourcesText.text = PlayerPrefs.GetFloat(GameplayController.PREFS_PLAYER_RESOURCES).ToString();
        buyStationText.text = "Buy Station: " + GameplayController.stationPrice;
        stationInfoText.text = "Stations – " + GameplayController.numberOfStations + " | " 
            + GameplayController.stationResourcesPerSecond + " Per Second";
        upgradeToolText.text = "Upgrade Tool: " + GameplayController.toolUpgradePrice;
        toolInfoText.text = "Tool Multiplier: " + GameplayController.toolMultiplier;
    }
}
