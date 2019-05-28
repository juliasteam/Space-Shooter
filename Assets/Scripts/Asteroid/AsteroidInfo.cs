using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "New Asteroid", menuName = "Asteroid")]
    public class AsteroidInfo : ScriptableObject
    {
        public string Name;
        public AsteroidData.TypeAsteroid Type;
        public int HP;
        public float Speed;
        public int Damage;

      
    }
}
