using UnityEngine.UI;
using UnityEngine;

namespace Shooter
{
    public class InfoPanel : MonoBehaviour
    {
        public bool isShow;

        [SerializeField]
        private Text score;
        [SerializeField]
        private Text hp;
        [SerializeField]
        private Text speed;
        [SerializeField]
        private Text fireRate;

        public void SetInfo(int _hp, int _score, float _speed, float _fireRate)
        {
            score.text = _score.ToString();
            hp.text = _hp.ToString();
            speed.text = _speed.ToString();
            fireRate.text = _fireRate.ToString();
        }
    }
}
