using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public static class BulletPool
    {
        private static bool init = false;

        private static GameObject bullet;
        private static SimpleBulletPool pool;

        public static void Initialize(Bullet.TypeBullet typeBullet, Bullet _bulletData, Transform perent)
        {
            init = true;
            switch (typeBullet)
            {
                case Bullet.TypeBullet.Low:
                    bullet = Resources.Load<GameObject>("For pools/Bullet/Fireball");

                    break;
                case Bullet.TypeBullet.Medium:
                    bullet = Resources.Load<GameObject>("For pools/Bullet/Fireball2");
                    break;
                case Bullet.TypeBullet.High:
                    bullet = Resources.Load<GameObject>("For pools/Bullet/Fireball3");
                    break;
                default:
                    bullet = Resources.Load<GameObject>("For pools/Bullet/Fireball");
                    break;
            }

            pool = new SimpleBulletPool(bullet, 30, _bulletData, perent);
        }

        public static BulletView GetBullet()
        {
            return pool.GetObject();
        }

        public static void PutBullet(BulletView bullet)
        {
            pool.PutObject(bullet);
        }
    }
}
