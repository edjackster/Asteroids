using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;
using Zenject;

public class AdMobProvider
{
    private const string AdUnitId = "ca-app-pub-3940256099942544/1033173712";
    
    private readonly SignalBus _signalBus;
    
    private InterstitialAd _interstitialAd;
    private bool _initialized;

    public AdMobProvider(SignalBus signalBus)
    {
        _signalBus = signalBus;
        Initialize();
    }

    public void LoadInterstitial()
    {
        if (!_initialized) return;

        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        var request = new AdRequest();

        InterstitialAd.Load(AdUnitId, request, OnAdLoad);
    }

    public void ShowInterstitial()
    {
        if (_interstitialAd != null &&
            _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial not ready");
        }
    }

    private void Initialize()
    {
        MobileAds.Initialize(initStatus =>
        {
            _initialized = true;

            LoadInterstitial();

            Debug.Log("AdMob initialized");
        });
    }

    private void OnAdLoad(InterstitialAd ad, LoadAdError error)
    {
        if (error != null || ad == null)
        {
            Debug.LogError($"Interstitial load failed: {error}");
            return;
        }

        _interstitialAd = ad;

        _interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            LoadInterstitial();
            OnAdFullScreenContentClosed().Forget();

        };

        _interstitialAd.OnAdFullScreenContentFailed += error =>
        {
            Debug.LogError(error);
            LoadInterstitial();
        };
    }
    
    private async UniTaskVoid OnAdFullScreenContentClosed()
    {
        await UniTask.Yield();
        _signalBus.Fire(new AdEndSignal());
    }
}
