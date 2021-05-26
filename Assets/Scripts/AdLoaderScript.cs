using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdLoaderScript : MonoBehaviour
{
    string gameId = "4143547";
    bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowAdd());
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    IEnumerator ShowAdd()
    {
        yield return new WaitForSeconds(1f);

        ShowInterstitialAd();
    }
}
