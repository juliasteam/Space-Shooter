using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Shooter
{
    public class Ship
    {
        public delegate void DeathHandler();
        public event DeathHandler DeathEvent;     // событие о смерти

        public event EventHandler<ShipInfoArgs> ShipInfoEvent = (sender, e) => { };  // событие на показ информации об игроке

        private ShipData data;
        private Bullet bullet;

        public Ship(ShipData shipData, Bullet _bullet)
        {
            data = shipData;
            bullet = _bullet;
        }

        public int GetHP()
        {
            return data.HP;
        }
        public float GetSpeed()
        {
            return data.Speed;
        }
       public int GetScore()
        {
            return data.Score;
        }
        public float GetFireRate()
        {
            return data.FireRate;
        }
        public float GetBulletSpeed()
        {
            return bullet.Speed;
        }
        public int GetBulletSorting()
        {
            return bullet.SortingOrder;
        }
        public Bullet.TypeBullet GetTypeBullet()
        {
            return bullet.Type;
        }

        /// <summary>
        /// Получение урона
        /// </summary>
        public void TakeDamage(int damage)
        {
            data.HP -= damage;
            if(data.HP <= 0)
            {
                data.HP = 0;
                DeathEvent();
            }
            ShipInfoEvent(this, new ShipInfoArgs(GetHP(), GetScore(), GetSpeed(), GetFireRate()));
        }

        public void ChangeScore(int inc)
        {
            data.Score += inc;
            ShipInfoEvent(this, new ShipInfoArgs(GetHP(), GetScore(), GetSpeed(), GetFireRate()));
        }

        public void Move(Vector2 position)
        {
            data.PositionX = position.x;
            data.PositionY = position.y;
          
        }
    }
}
