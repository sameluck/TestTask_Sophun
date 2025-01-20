using System.Threading.Tasks;
using UnityEngine;
using SimplePopupManager;

public class ShowLeaderboard : MonoBehaviour
{
    private IPopupManagerService popupManagerService;
    [SerializeField] private Transform root;

    private void Start()
    {
        popupManagerService = new PopupManagerServiceService(root);
    }

    public async void DisplayLeaderboard()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Leaderboard");
        if (jsonFile == null)
        {
            Debug.LogError("Leaderboard JSON file not found!");
            return;
        }

        string jsonString = jsonFile.text;

        Debug.Log("Attempting to open leaderboard popup...");
        await OpenPopupAsync("LeaderboardPopup", jsonString);
        Debug.Log("Popup successfully opened!");
    }

    private Task OpenPopupAsync(string name, object param)
    {
        var tcs = new TaskCompletionSource<bool>();

        try
        {
            popupManagerService.OpenPopup(name, param);

            tcs.SetResult(true);
        }
        catch (System.Exception ex)
        {
            tcs.SetException(ex);
        }

        return tcs.Task;
    }
}