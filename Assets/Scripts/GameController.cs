using ChickenRoast.Chicken;
using ChickenRoast.Fire;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace ChickenRoast
{
    public class GameController : MonoBehaviour
    {
        public ChickenController chickenController;
        public FireController fireController;
        public Config config;

        public GameObject winTitle;
        public GameObject loseTitle;
        public Button restartButton;

        private void Start()
        {
            fireController.Setup(config.fireData, onFireShutDown: LoseGame);
            chickenController.Setup(config.chickenData, fireController.GetIntensity,WinGame,LoseGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        private void LoseGame()
        {
            DisableControllers();
            loseTitle.transform.DOScale(1,0.5f).SetEase(Ease.OutBounce);
        }

        private void WinGame()
        {
            DisableControllers();
            winTitle.transform.DOScale(1,0.5f).SetEase(Ease.OutBounce);
        }

        private void DisableControllers()
        {
            fireController.enabled = false;
            chickenController.enabled = false;
        }

        private void RestartGame() => SceneManager.LoadScene(0);
    }
}