using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressableTester : MonoBehaviour
{
    public string popupAddress = "LeaderboardPopup";

    private async void Start()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(popupAddress);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Addressable asset loaded successfully.");
        }
        else
        {
            Debug.LogError($"Failed to load Addressable asset. Status: {handle.Status}");
        }
    }
}