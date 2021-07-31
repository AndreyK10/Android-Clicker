using System.Collections;
using UnityEngine;

public class GameplayController : MonoBehaviour
{    
    public static int numberOfStations { get; private set; }
    public static float toolMultiplier { get; private set; }
    public static float stationPrice { get; private set; }
    public static float toolUpgradePrice { get; private set; }
    public static float stationResourcesPerSecond { get; private set; }

    [SerializeField] private float playerResources;
    [SerializeField] private float stationPriceGrowthRate = 200f;
    [SerializeField] private int baseStationMultiplier = 1;
    [SerializeField] private float baseToolMultiplier = 1f;
    [SerializeField] private float toolUpgradeGrowthRate = 200f;
    [SerializeField] private float defaultStationPrice = 500f;
    [SerializeField] private float defaultToolPrice = 500f;
    [SerializeField] private float defaultToolMultiplier = 1f;

    private bool canMine;

    public const string PREFS_PLAYER_RESOURCES = "Money_v1.0";
    public const string PREFS_TOOL_MULTIPLIER = "Multiplier_v1.0";
    public const string PREFS_STATIONS = "Stations_v1.0";
    public const string PREFS_STATION_PRICE = "StationPrice_v1.0";
    public const string PREFS_TOOL_PRICE = "ToolPrice_v1.0";

    private void Awake()
    {
        playerResources = PlayerPrefs.GetFloat(PREFS_PLAYER_RESOURCES);
        toolMultiplier = PlayerPrefs.GetFloat(PREFS_TOOL_MULTIPLIER, defaultToolMultiplier);
        numberOfStations = PlayerPrefs.GetInt(PREFS_STATIONS);
        stationPrice = PlayerPrefs.GetFloat(PREFS_STATION_PRICE, defaultStationPrice);
        toolUpgradePrice = PlayerPrefs.GetFloat(PREFS_TOOL_PRICE, defaultToolPrice);
    }

    private void Update()
    {
        if (!canMine)
        {
            canMine = true;
            StartCoroutine(GetFromStation(numberOfStations));
        }
        stationResourcesPerSecond = baseStationMultiplier * numberOfStations;
    }

    public void AddResources()
    {
        playerResources += baseToolMultiplier * toolMultiplier;
        PlayerPrefs.SetFloat(PREFS_PLAYER_RESOURCES, playerResources);
    }

    private bool TryRemoveResources(float upgradePrice)
    {
        if (playerResources >= upgradePrice)
        {
            playerResources -= upgradePrice;
            PlayerPrefs.SetFloat(PREFS_PLAYER_RESOURCES, playerResources);
            return true;            
        }
        else
        {
            return false;
        }
    }

    private IEnumerator GetFromStation(int numberOfStations)
    {
        playerResources += baseStationMultiplier * numberOfStations;        
        PlayerPrefs.SetFloat(PREFS_PLAYER_RESOURCES, playerResources);
        yield return new WaitForSeconds(1);
        canMine = false;
    }

    public void BuyStation()
    {
        if (TryRemoveResources(stationPrice))
        {
            numberOfStations++;
            PlayerPrefs.SetInt(PREFS_STATIONS, numberOfStations);
            stationPrice += stationPriceGrowthRate;
            PlayerPrefs.SetFloat(PREFS_STATION_PRICE, stationPrice);
        }
    }

    public void UpgradeTool()
    {
        if (TryRemoveResources(toolUpgradePrice))
        {
            toolMultiplier++;
            PlayerPrefs.SetFloat(PREFS_TOOL_MULTIPLIER, toolMultiplier);
            toolUpgradePrice += toolUpgradeGrowthRate;
            PlayerPrefs.SetFloat(PREFS_TOOL_PRICE, toolUpgradePrice);
        }
    }
}
