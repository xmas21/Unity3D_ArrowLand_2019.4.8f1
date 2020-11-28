using UnityEngine;
using UnityEngine.Advertisements;

public class ADmanager : MonoBehaviour, IUnityAdsListener
{
    private string googleID = "3854575";
    private string placementRevival = "revival";
    private Player player;

    private void Start()
    {
        Advertisement.Initialize(googleID, false); // 廣告初始化
        Advertisement.AddListener(this);

        player = FindObjectOfType<Player>();

    }

    /// <summary>
    /// 顯示廣告
    /// </summary>
    public void ShowRevivalAD()
    {
        if (Advertisement.IsReady(placementRevival))
        {
            Advertisement.Show(placementRevival);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == placementRevival)
        {
            switch (showResult)
            {
                case ShowResult.Failed:         // 狀況 1 失敗：
                    print("廣告失敗");
                    break;
                case ShowResult.Skipped:        // 狀況 2 略過：
                    print("廣告略過");
                    break;
                case ShowResult.Finished:       // 狀況 3 失敗：
                    print("廣告成功");
                    player.Revival();
                    break;
            }
        }
    }

}
