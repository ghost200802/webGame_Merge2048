using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UomaWeb;

namespace FMGames {

    public class GameManager : MonoBehaviour {
        public static GameManager _instance;

        [Space(12)]
        public bool isPaused;
        private int levelNum;

        [SerializeField] GameObject tutorial;

        [SerializeField] Level level;
        private void Awake() {
            _instance = this;

            Application.targetFrameRate = 60;
        }

        // Start is called before the first frame update
        void Start() {
            StartCoroutine(UomaController.Instance.GetVirtualCurrency((int currency) =>
            {
                StartCoroutine(UomaController.Instance.GetCompleteLevel((int result) =>
                {
                    levelNum = UomaDataManager.CompleteLevel;

                    GameUi._instance.InitLevel(levelNum);
                    level.Init(levelNum);
                }));
            }));
        }

        /// <summary>
        /// Called when the player wins a level
        /// </summary>
        private void Victory() {
            StartCoroutine(UomaController.Instance.CompleteLevel(UomaDataManager.CurrLevel, 3, (result) =>
            {
                if (result.successCode == 0)
                {
                    // ChestManager._instance.AddProgression(Random.Range(18, 25));
                    GameUi._instance.ShowVictory();
                }
            }));
            
        }

        /// <summary>
        /// Called every time when the player either completes a level or looses it
        /// </summary>
        /// <param name="isCompleted"></param>
        public void GameOver(bool isCompleted) {
            if (isPaused) return;

            isPaused = true;

            if (isCompleted) {
                Victory();
                return;
            }

            GameUi._instance.ShowGameOver();
        }

        public void StartGame() {
            isPaused = false;

            tutorial.SetActive(false);
        }

        public Level GetLevel() => level;
    }
}