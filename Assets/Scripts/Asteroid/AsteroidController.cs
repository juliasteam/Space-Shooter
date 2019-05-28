using UnityEngine;

namespace Shooter
{
    public class AsteroidController
    {
        public AsteroidView asteroidView;
        private AsteroidData asteroid;
        private int resetHP;

        public AsteroidController(AsteroidData _asteroid, AsteroidView _asteroidView)
        {
            this.asteroid = _asteroid;
            this.asteroidView = _asteroidView;
            asteroidView.SetDamage(asteroid.Damage);
            resetHP = _asteroid.HP;
        }

        public void Reset()
        {
            asteroid.HP = resetHP;
        }

        public void OffObject(Vector3 startPosition)
        {
            asteroidView.HpEvent -= OnHpChange;
            asteroidView.DestroyEvent -= OnDestroyAsteroid;
            asteroidView.DeathEvent -= OnDeath;
            asteroid.DeathEvent -= OnDeath;
            asteroidView.SetStartPosition(startPosition);
            asteroidView.gameObject.SetActive(false);
        }
        public void OnObject()
        {
            asteroidView.HpEvent += OnHpChange;
            asteroidView.DeathEvent += OnDeath;
            asteroidView.DestroyEvent += OnDestroyAsteroid;
            asteroid.DeathEvent += OnDeath;
            asteroidView.gameObject.SetActive(true);
            asteroidView.StartStay();
        }

        public void Move()
        {
            asteroidView.Move(asteroid.Speed);
        }

        private void OnHpChange(int damage)
        {
            asteroid.TakeDamage(damage);
        }
       
        private void OnDeath()
        {
            asteroidView.AnimateDeath();
        }

        private void OnDestroyAsteroid()
        {
            AsteroidPool.PutAsteroid(this);
        }

        private void OffAsteroid()
        {
            AsteroidPool.PutAsteroid(this);
        }
    }
}
