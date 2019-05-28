using UnityEngine;

namespace Shooter
{
    public class AsteroidData
    {
        public int HP;
        public float Speed;
        public int Damage;
        public TypeAsteroid Type;

        public delegate void DeathHandler();
        public event DeathHandler DeathEvent;     // событие о смерти

        public enum TypeAsteroid
        {
            Asteroid1 = 1,
            Asteroid2,
            Asteroid3
        }

        public AsteroidData(int hp, float speed, int damage, TypeAsteroid type)
        {
            this.HP = hp;
            this.Speed = speed;
            this.Damage = damage;
            this.Type = type;
        }

        /// <summary>
        /// Получение урона
        /// </summary>
        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                DeathEvent?.Invoke();
            }

        }
    }
}
