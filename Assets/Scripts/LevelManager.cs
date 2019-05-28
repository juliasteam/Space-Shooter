using UnityEngine;
using UnityEngine.UI;

namespace Shooter
{
    public class LevelManager : MonoBehaviour
    {
        public Button[] LevelsButton;
        public Sprite Lock;
        public Sprite Pass;
        public Sprite On;

        public enum ButtonStay
        {
            Lock = 0,  // уровень не доступен
            On = 1,        // уровень открыт 
            Pass = 2       // уровень пройден
        }
        public enum LevelStay
        {
            Win = 1,      // уровень выигран
            Lose = 2,     // уровень проигран 
            Save = 3      // уровень сохранен
        }
        void Start()
        {
            if(!PlayerPrefs.HasKey("Level1"))
            {
                PlayerPrefs.SetInt("Level1", 1);
            }
            
            for (int i=0; i< LevelsButton.Length; i++)
            {
                string level = (i + 1).ToString();
                int k = i;
                if (PlayerPrefs.GetInt("Level" + level) == (int)ButtonStay.Lock)
                {
                    LevelsButton[i].image.sprite = Lock;
                }
                else
                {
                    LevelsButton[k].onClick.AddListener(() =>
                    {
                        BaseProfile.instance.CurrentLevel = k + 1;
                        MySceneManager.LoadSceneS("Game");
                    });
                }
                if (PlayerPrefs.GetInt("Level" + level) == (int)ButtonStay.Pass)
                {
                    LevelsButton[i].image.sprite = Pass;
                }
                if (PlayerPrefs.GetInt("Level" + level) == (int)ButtonStay.On)
                {
                    LevelsButton[i].image.sprite = On;
                }
            }
           
        }

       
    }
}
