using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.Networking;

public class PlayerItemUI : MonoBehaviour
{
    public TextMeshProUGUI nameObject;
    public TextMeshProUGUI scoreObject;
    public Image avatarImage;
    public Image background;

    public async Task SetData(LeaderboardData player)
    {
        nameObject.text = player.name;
        scoreObject.text = player.score.ToString();
        SetTypeStyle(player.type);

        await LoadAvatar(player.avatar, player.name);
    }

    private void SetTypeStyle(string type)
    {
        float newHeight;

        switch (type)
        {
            case "Diamond":
                background.color = Color.cyan;
                newHeight = 180f;
                break;
            case "Gold":
                background.color = Color.yellow;
                newHeight = 160f;
                break;
            case "Silver":
                background.color = Color.gray;
                newHeight = 140f;
                break;
            case "Bronze":
                background.color = new Color(0.8f, 0.5f, 0.2f);
                newHeight = 120f;
                break;
            default:
                background.color = Color.white;
                newHeight = 100f;
                break;
        }

        // Adjust the RectTransform size
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
        }
    }


    private async Task LoadAvatar(string url, string name)
    {
        var cachedPath = Path.Combine(Application.persistentDataPath , name);
        //if (File.Exists(cachedPath))
        //{ 
        //    //var texture =;
        //    //avatarImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        //}
        using (var www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET))
        {
            www.downloadHandler = new DownloadHandlerTexture();
            var request = www.SendWebRequest();

            while (!request.isDone)
                await Task.Yield();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                avatarImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                File.WriteAllTextAsync(cachedPath, www.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Failed to load avatar from {url}");
                avatarImage.color = Color.red;
            }
        }
    }
}
