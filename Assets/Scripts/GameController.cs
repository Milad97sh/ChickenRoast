using ChickenRoast.Chicken;
using ChickenRoast.Data;
using ChickenRoast.Fire;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace ChickenRoast
{
    public class GameController : MonoBehaviour
    {
        public ChickenController chickenController;
        public FireController fireController;
        public DataConfig dataConfig;

        public GameObject winPanel;
        public GameObject losePanel;
        public TextMeshProUGUI loseTitle;
        public Button restartButton;

        private void Start()
        {
            fireController.Setup(dataConfig.fireData, onFireShutDown: LoseGame);
            chickenController.Setup(dataConfig.chickenData, fireController.GetIntensity,WinGame,LoseGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        private void LoseGame()
        {
            DisableControllers();
            LoseHandler loseHandler = new LoseHandler(loseTitle);
            loseHandler.LoseUpdate();
            losePanel.transform.DOScale(1,0.5f).SetEase(Ease.OutBounce);
            restartButton.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetDelay(2);
        }

        private void WinGame()
        {
            DisableControllers();
            winPanel.transform.DOScale(1,0.5f).SetEase(Ease.OutBounce);
            restartButton.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetDelay(2);
        }

        private void DisableControllers()
        {
            fireController.enabled = false;
            chickenController.enabled = false;
        }

        private void RestartGame() => SceneManager.LoadScene(0);
    }
}