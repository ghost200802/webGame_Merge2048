using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace FMGames {
    public class Ads : MonoBehaviour {

        // Use this for initialization
        void Start() {
#if UNITY_IOS
        Advertisement.Initialize("4147408");
#elif UNITY_ANDROID
            Advertisement.Initialize("4147409");
#endif

        }

        public float timeToShowAds = 180;
        float timer;

        // Update is called once per frame
        void Update() {
            if (timer > timeToShowAds) {
                timer = 0;
                //Advertisement.Show();
            }
        }
    }
}