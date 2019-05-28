using System;
using UnityEngine;

namespace Shooter
{
    public class ShipInfoArgs : EventArgs
    {
        public int HP;
        public int Score;
        public float Speed;
        public float FireRate;

        public ShipInfoArgs(int hp, int score, float speed, float fireRate)
        {
            this.HP = hp;
            this.Score = score;
            this.Speed = speed;
            this.FireRate = fireRate;

        }
    }
}
