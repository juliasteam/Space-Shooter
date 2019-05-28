using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class ShipView : MonoBehaviour
    {
        public delegate void FireHandler();
        public event FireHandler FireEvent;     // событие стрельбы

        public delegate void HPHandler(int damade);
        public event HPHandler HpEvent;     // событие на изменение жизней. Попадание в корабль

        public delegate void MoveHandler(float offset);
        public event MoveHandler MoveEvent;     // событие на движение корабля.

        public delegate void ChangePositionHandler(Vector2 position);
        public event ChangePositionHandler ChangePositionEvent;     // событие на изменение позиции.

        [SerializeField]
        private BulletView bulletPrefab;
        [SerializeField]
        private Transform[] bulletStartPoses;

        [SerializeField]
        private AudioSource shotSND;
        [SerializeField]
        private AudioSource explosionSND;

        private Rigidbody2D rigidbody;
        private Animator animator;
        private float leftPoint;
        private float rightPoint;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
            leftPoint = -rightPoint;
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Генерация события перемещения
        /// </summary>
        public void Move(float offset)
        {
            MoveEvent?.Invoke(offset);
        }

        /// <summary>
        /// Генерация события стрельбы
        /// </summary>
        public void Fire()
        {
            FireEvent?.Invoke();
        }

        /// <summary>
        /// Анимация движения
        /// </summary>
        public void AnimateMove(float offset, float speed)
        {
            rigidbody.velocity = new Vector2(offset * speed, 0);
            rigidbody.position = new Vector2
            (
                Mathf.Clamp(rigidbody.position.x, leftPoint, rightPoint),
                rigidbody.position.y
            );
            ChangePositionEvent?.Invoke(rigidbody.position);
        }

        /// <summary>
        /// Анимация выстрела
        /// </summary>
        public void AnimateFire(int bulletSortingOrder, float bulletSpeed)
        {
            BulletView bullet = BulletPool.GetBullet();
            bullet.Shot(bulletSortingOrder, bulletSpeed);
            if (!shotSND.isPlaying)
            {
                shotSND.Play();
            }
        }

        /// <summary>
        /// Анимация смерти
        /// </summary>
        public void AnimateDeath()
        {
            animator.SetBool("expl", true);
            explosionSND.Play();
        }

        public void OffShip()
        {
            gameObject.SetActive(false);
        }
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // столкновение с астероидом 
            if (collision.tag == "Asteroid")
            {
                AsteroidView asteroid = collision.GetComponent<AsteroidView>();
                HpEvent?.Invoke(asteroid.GetAsteroidDamage());
                asteroid.KillAsteroid();
                
            }
        }


    }
}
