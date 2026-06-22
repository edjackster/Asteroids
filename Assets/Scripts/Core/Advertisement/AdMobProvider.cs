using Core.Configs;
using Core.Signals;
using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;
using Zenject;

namespace Core.Advertisement
{
    public class AdMobProvider
    {
        private readonly SignalBus _signalBus;
        private InterstitialAd _interstitialAd;
        private bool _initialized;
        private AdMobConfig _config;

        public AdMobProvider(SignalBus signalBus, AdMobConfig config)
        {
            _signalBus = signalBus;
            _config = config;
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

            InterstitialAd.Load(_config.AdMobId, request, OnAdLoad);
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
                OnAdFullScreenContentFailed().Forget();
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
                OnAdFullScreenContentFailed().Forget();
            };
        }
    
        private async UniTask OnAdFullScreenContentClosed()
        {
            await UniTask.Yield();
            _signalBus.Fire(new AdEndSignal());
        }
    
        private async UniTask OnAdFullScreenContentFailed()
        {
            await UniTask.Yield();
            _signalBus.Fire(new AdFailedSignal());
        }
    }
}
