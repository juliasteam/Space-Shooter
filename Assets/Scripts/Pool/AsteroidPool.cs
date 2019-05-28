using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public static class AsteroidPool 
    {
        private static bool init = false;

        private static GameObject asteroid;
        private static SimpleObjectPool pool;

        public static void Initialize(AsteroidData.TypeAsteroid typeAsteroid, AsteroidData _asteroidData, Vector2 maxPosition)
        {
            init = true;
            switch (typeAsteroid)
            {
                case AsteroidData.TypeAsteroid.Asteroid1:
                    asteroid = Resources.Load<GameObject>("For pools/Asteroid/Asteroid");

                    break;
                case AsteroidData.TypeAsteroid.Asteroid2:
                    asteroid = Resources.Load<GameObject>("For pools/Asteroid/Asteroid2");
                    break;
                case AsteroidData.TypeAsteroid.Asteroid3:
                    asteroid = Resources.Load<GameObject>("For pools/Asteroid/Asteroid3");
                    break;
                default:
                    asteroid = Resources.Load<GameObject>("For pools/Asteroid/Asteroid");
                    break;
            }

            pool = new SimpleObjectPool(asteroid, 10, _asteroidData, maxPosition);
        }

        public static AsteroidController GetAsteroid()
        {
            AsteroidController newAsteroid = pool.GetObject();
            return newAsteroid;
        }

        public static void PutAsteroid(AsteroidController asteroid)
        {
            asteroid.asteroidView.ScoreEvent = null;
            pool.PutObject(asteroid);
        }
    }
}
