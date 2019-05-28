using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
    public class BulletInfo : ScriptableObject
    {
        public string Name;
        public float Speed;
        public int SortingOrder;
        public int Power;
        public Bullet.TypeBullet Type;
    }
}
