using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Shooter
{
    public class LevelController : MonoBehaviour
    {
        public int CurrentLevel;
        [SerializeField]
        private UIManager uiManager;
        [SerializeField]
        private ShipView shipPref;
        private Ship ship;
        private ShipController shipController;

        [SerializeField]
        private AsteroidInfo[] asteroidInfo;
        private AsteroidData asteroidData;
        private Bullet bullet;
        [SerializeField]
        private BulletInfo[] bulletInfo;
        [SerializeField]
        private Transform bulletPosition;
        private int taskPoint;

        [SerializeField]
        private AudioSource finalSND;

        private float rightPoint;
        private float upPoint;

        void Awake()
        {
            rightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x - 0.5f;
            upPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y + 1f;
        }

        void Start()
        {
            CurrentLevel = BaseProfile.instance.CurrentLevel;
            SetTaskPoint();
            uiManager.SetLevel(CurrentLevel);
            uiManager.SetTask(taskPoint);
            Load(CurrentLevel);
            shipController.FinishEvent += Finish;
            PlayerPrefs.SetInt("LevelStay" + CurrentLevel, (int)LevelManager.LevelStay.Save);
        }

        public void Spawn()
        {
            // генерируем пул астероидов
            Vector2 maxPosition = new Vector2(rightPoint, upPoint);
            AsteroidPool.Initialize(asteroidData.Type, asteroidData, maxPosition);

            // генерируем пул пуль
            BulletPool.Initialize(bullet.Type, bullet, bulletPosition);
            StartCoroutine("SpawnAsteroid");
        }

        /// <summary>
        /// загружаем уровень
        /// </summary>
        public void Load(int level)
        {
            // В зависимости от уровня загружаем нужный тип пуль
            bullet = new Bullet(bulletInfo[level-1].Speed, bulletInfo[level-1].SortingOrder, bulletInfo[level-1].Power, bulletInfo[level-1].Type);

            // загружаем данные игрока
            IFormatter formatter = new BinaryFormatter();
           
            string path = "";
            // при первом заходе на уровень загружаем дефолтные данные
            if (!PlayerPrefs.HasKey("Level" + level + "Default"))
            {
                path = Application.streamingAssetsPath + "/saveLavel" + CurrentLevel.ToString() + "Default.txt";
                PlayerPrefs.SetInt("Level" + level + "Default", 1);
            }
            else
            {
                if (PlayerPrefs.GetInt("LevelStay" + level) == (int)LevelManager.LevelStay.Save)
                {
                    path = Application.streamingAssetsPath + "/saveLavel" + CurrentLevel.ToString() + ".txt";
                }
                else
                {
                    path = Application.streamingAssetsPath + "/saveLavel" + CurrentLevel.ToString() + "Default.txt";
                }
               
            }
            FileStream buffer = File.OpenRead(path);
            ShipData data = formatter.Deserialize(buffer) as ShipData;
           
            ship = new Ship(data, bullet);
            buffer.Close();
            shipController = new ShipController(ship, shipPref);
            ShowShipInfo(data.HP, data.Score, data.Speed, data.FireRate);
            ship.ShipInfoEvent += OnShipInfo;
            // В зависимости от уровня загружаем нужный тип астероидов
            asteroidData = new AsteroidData(asteroidInfo[level-1].HP, asteroidInfo[level-1].Speed, asteroidInfo[level-1].Damage, asteroidInfo[level-1].Type);
           
            Spawn();
        }

        /// <summary>
        /// Сохраняем уровень
        /// </summary>
        public void Save()
        {
            Debug.Log("SaveProgress");
            IFormatter formatter = new BinaryFormatter();
            FileStream buffer = File.Create(Application.streamingAssetsPath + "/saveLavel" + CurrentLevel.ToString()+ ".txt");
            formatter.Serialize(buffer, shipController.GetShipData());
            buffer.Close();
            if (PlayerPrefs.GetInt("LevelStay" + CurrentLevel) != (int)LevelManager.LevelStay.Win   && PlayerPrefs.GetInt("LevelStay" + CurrentLevel) != (int)LevelManager.LevelStay.Lose)
            {
                PlayerPrefs.SetInt("LevelStay" + CurrentLevel, (int)LevelManager.LevelStay.Save);
            }
        }

        public void SetTaskPoint()
        {
            if (CurrentLevel == 1)
            {
                taskPoint = Constant.taskLevel1;
            }
            if (CurrentLevel == 2)
            {
                taskPoint = Constant.taskLevel2;
            }
            if (CurrentLevel == 3)
            {
                taskPoint = Constant.taskLevel3;
            }
        }
        IEnumerator SpawnAsteroid()
        {
            while (true)
            {
                Create();
                yield return new WaitForSeconds(Random.Range(1f, 4f));
            }
        }
        private void Create()
        {
            AsteroidController asteroidController = AsteroidPool.GetAsteroid();
            asteroidController.Move();
            asteroidController.asteroidView.ScoreEvent += OnScore;
        }

        private void OnScore(int inc)
        {
            shipController.OnChangeScore(inc);
        }

        private void OnShipInfo(object sender, ShipInfoArgs args)
        {
            uiManager.SetInfo(args.HP, args.Score, args.Speed, args.FireRate);
            Save();
            if (args.Score >= taskPoint)
            {
                Finish(true);
            }
        }

        private void ShowShipInfo(int hp, int score, float speed, float fireRate)
        {
            uiManager.SetInfo(hp, score, speed, fireRate);
        }

        private void Finish(bool success)
        {
            StopCoroutine("SpawnAsteroid");
            uiManager.ShowFinish(success);
            StopGame();
            Debug.Log("Finish");
            if (success)
            {
                int nextLevel = CurrentLevel;
                nextLevel++;
                PlayerPrefs.SetInt("Level" + CurrentLevel, (int)LevelManager.ButtonStay.Pass);
                if (PlayerPrefs.GetInt("Level" + nextLevel) == (int)LevelManager.ButtonStay.Lock)
                {
                    PlayerPrefs.SetInt("Level" + nextLevel, (int)LevelManager.ButtonStay.On);
                    Debug.Log("NextLevel" + nextLevel.ToString() + " = " + LevelManager.ButtonStay.On.ToString());
                }
                PlayerPrefs.SetInt("LevelStay" + CurrentLevel, (int) LevelManager.LevelStay.Win); // выиграли
            }
            else
            {
                PlayerPrefs.SetInt("LevelStay" + CurrentLevel, (int)LevelManager.LevelStay.Lose); // проиграли
            }
            if(!finalSND.isPlaying)
            {
                finalSND.Play();
            }
        }

        private void StopGame()
        {
            shipController.StopEvent();
            shipController.FinishEvent -= Finish;
            shipController.OfShip();
        }
    }
}
