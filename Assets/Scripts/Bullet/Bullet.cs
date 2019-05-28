using UnityEngine;

namespace Shooter
{
    public class Bullet
    {
        public float Speed;
        public int SortingOrder;
        public int Power;
        public TypeBullet Type;

        public enum TypeBullet
        {
            Low = 1,
            Medium,
            High
        }

        public Bullet(float speed, int sortingOrder, int power, TypeBullet type)
        {
            this.Speed = speed;  
            this.SortingOrder = sortingOrder;
            this.Power = power;
            this.Type = type;
        }
    }
}
