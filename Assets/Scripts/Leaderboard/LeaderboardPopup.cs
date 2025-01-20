using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using SimplePopupManager;

public class LeaderboardPopup : MonoBehaviour, IPopupInitialization
{
    private IPopupManagerService popupManagerService;
    [SerializeField] private Transform root;

    private GameObject currentObject;
    
    public Transform content; //  Place for leaderboard entries
    public GameObject playerItemPrefab; // Prefab for leaderboard entries

    
    private void Start()
    {
        popupManagerService = new PopupManagerServiceService(root);
        currentObject = this.gameObject;
    }
    public async Task Init(object param)
    {
        if (param is string jsonString)
        {
            List<LeaderboardData> leaderboardData = ParseLeaderboardJson(jsonString);

            foreach (Transform child in content)
                Destroy(child.gameObject);

            foreach (var player in leaderboardData)
            {
                var playerItem = Instantiate(playerItemPrefab, content);
                var playerUI = playerItem.GetComponent<PlayerItemUI>();
                await playerUI.SetData(player); // Load and set data asynchronously
            }
        }
        else
        {
            Debug.LogError("Invalid parameter passed to LeaderboardPopup.");
        }
    }

    private List<LeaderboardData> ParseLeaderboardJson(string json)
    {
        LeaderboardWrapper leaderboardWrapper = JsonUtility.FromJson<LeaderboardWrapper>(json);
        return new List<LeaderboardData>(leaderboardWrapper.leaderboard);
    }
    
    

    public async void CloseLeaderboard()
    {
        popupManagerService.ClosePopup("LeaderboardPopup");
        if (currentObject != null)
        {
            Destroy(currentObject);
        }
    }
}

[System.Serializable]
public class LeaderboardWrapper
{
    public LeaderboardData[] leaderboard;
}

[System.Serializable]
public class LeaderboardData
{
    public string name;
    public int score;
    public string avatar;
    public string type;
}