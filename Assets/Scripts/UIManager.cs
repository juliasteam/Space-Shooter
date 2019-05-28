using UnityEngine.UI;
using UnityEngine;

namespace Shooter
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Text currentLevel;
        [SerializeField]
        private Text task;
        [SerializeField]
        private Text score;
        [SerializeField]
        private GameObject scoreImage;
        [SerializeField]
        private Text lifes;
        [SerializeField]
        private GameObject lifesImage;
        [SerializeField]
        private Button infoButton;
        [SerializeField]
        private InfoPanel infoPanel;
        [SerializeField]
        private GameObject taskPanel;
        [SerializeField]
        private GameObject finishWinPopup;
        [SerializeField]
        private GameObject finishLosePopup;

        void Start()
        {
            infoButton.onClick.AddListener(() =>
            {
                    infoPanel.gameObject.SetActive(!infoPanel.isShow);
                    infoPanel.isShow = !infoPanel.isShow;
            });
        }
        /// <summary>
        /// Устанавливаем информацию о корабле
        /// </summary>
        public void SetInfo(int _hp, int _score, float _speed, float _fireRate)
        {
            score.text = _score.ToString();
            lifes.text = _hp.ToString();
            infoPanel.SetInfo(_hp,  _score,  _speed,  _fireRate);
        }

        public void SetLevel(int level)
        {
            currentLevel.text = level.ToString();
        }
        public void ShowFinish(bool success)
        {
            infoButton.gameObject.SetActive(false);
            infoPanel.gameObject.SetActive(false);
            scoreImage.gameObject.SetActive(false);
            lifesImage.gameObject.SetActive(false);
            if (success)
            {
                finishWinPopup.SetActive(true);
            }
            else
            {
                finishLosePopup.SetActive(true);
            }
        }

        public void SetTask(int points)
        {
            task.text = points.ToString();
            taskPanel.SetActive(true);
            Invoke("OffTask", 3f);
        }

        private void OffTask()
        {
            taskPanel.SetActive(false);
        }
    }
}
