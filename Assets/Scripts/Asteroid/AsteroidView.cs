using UnityEngine;

namespace Shooter
{
    public class AsteroidView : MonoBehaviour
    {

        public delegate void HPHandler(int damade);
        public event HPHandler HpEvent;     // событие попали в астероид

        public delegate void DestroyHandler();
        public event DestroyHandler DestroyEvent;     // событие уничтожения астероида

        public delegate void DeathHandler();
        public event DeathHandler DeathEvent;     // событие смерти астероида

        public delegate void ScoreHandler(int inc);

        [SerializeField]
        private SpriteRenderer spriteRanderer;
        private Rigidbody2D rigidbody;
        private CircleCollider2D collider;
        private int asteroidDamage;
        private Animator animation;

        public ScoreHandler ScoreEvent;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<CircleCollider2D>();
            spriteRanderer = GetComponent<SpriteRenderer>();
            animation = GetComponent<Animator>();
        }

        public void Move(float speed)
        {
            rigidbody.velocity = speed * (-Vector2.up);
        }
        /// <summary>
        /// Генерация события разрушение астероида
        /// </summary>
        public void KillAsteroid()
        {
            collider.enabled = false;
            AnimateDeath();
        }
        public void SetStartPosition(Vector3 position)
        {
            transform.position = position;
        }
        public void SetDamage(int damage)
        {
            asteroidDamage = damage;
        }
        public void AnimateDeath()
        {
            rigidbody.velocity = Vector2.zero;
            animation.Play("Explosion");
            Invoke("DestroyAsteroid", 0.2f);
        }
        public void StartStay()
        {
            animation.Play("Idle");
            collider.enabled = true;
        }

        public int GetAsteroidDamage()
        {
            return asteroidDamage;
        }

        private void DestroyAsteroid()
        {
            DestroyEvent?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Respawn")
            {
                DestroyAsteroid();
            }
            if (collision.tag == "Bullet")
            {
                if(HpEvent != null)
                {
                    BulletView bullet = collision.gameObject.GetComponent<BulletView>();
                    HpEvent?.Invoke(bullet.GetDamage());
                    ScoreEvent?.Invoke(1);
                    bullet.DestroyBullet();
                }
            }
        }

    }
}
