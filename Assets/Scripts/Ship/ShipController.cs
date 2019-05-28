using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class ShipController
    {
        private Ship ship;
        private ShipView shipView;
        private float nextFire;

        public delegate void FinishHandler(bool success);
        public event FinishHandler FinishEvent;

        public ShipController(Ship _ship, ShipView _shipView)
        {
            this.ship = _ship;
            this.shipView = _shipView;
            shipView.FireEvent += OnFire;
            shipView.HpEvent += OnHpChange;
            shipView.MoveEvent += OnMove;
            shipView.ChangePositionEvent += OnPositionChange;
            ship.DeathEvent += OnDeath;
         
        }

        public ShipData GetShipData()
        {
            ShipData data = new ShipData(ship.GetHP(), ship.GetSpeed(), shipView.gameObject.transform.position.x, shipView.gameObject.transform.position.y, ship.GetFireRate(), ship.GetScore());
            return data;
        }

        public void OnChangeScore(int inc)
        {
            ship.ChangeScore(inc);
        }
        public void StopEvent()
        {
            shipView.FireEvent -= OnFire;
            shipView.HpEvent -= OnHpChange;
            shipView.MoveEvent -= OnMove;
            shipView.ChangePositionEvent -= OnPositionChange;
            ship.DeathEvent -= OnDeath;
        }
        public void OfShip()
        {
            shipView.OffShip();
        }

        private void OnMove(float offset)
        {
            shipView.AnimateMove(offset, ship.GetSpeed());
        }

        private void OnPositionChange(Vector2 position)
        {
            ship.Move(position);
        }
        private void OnFire()
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + ship.GetFireRate();
                shipView.AnimateFire(ship.GetBulletSorting(), ship.GetBulletSpeed());
            }
        }

        private void OnHpChange(int damage)
        {
            ship.TakeDamage(damage);
        }

        private void OnDeath()
        {
            shipView.AnimateDeath();
            shipView.FireEvent -= OnFire;
            shipView.HpEvent -= OnHpChange;
            shipView.MoveEvent -= OnMove;
            shipView.ChangePositionEvent -= OnPositionChange;
            ship.DeathEvent -= OnDeath;
            FinishEvent(false);
        }

       
    }
}
