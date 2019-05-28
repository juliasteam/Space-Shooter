using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class SimpleBulletPool
    {
        public GameObject _objectPrefab;
        private Bullet _bullet;
        private Queue<BulletView> objectStack;
        private Transform perent;

        public SimpleBulletPool(GameObject objectPrefab, int sizePool, Bullet bullet, Transform _position)
        {
            perent = _position;
            objectStack = new Queue<BulletView>();
            _objectPrefab = objectPrefab;
            _objectPrefab.SetActive(false);
            _bullet = bullet;
            for (int i = 0; i < sizePool; i++)
            {
                GameObject newObject = Object.Instantiate(_objectPrefab, perent.position, Quaternion.identity) as GameObject;
                BulletView bulletView = newObject.GetComponent<BulletView>();
                bulletView.SetDamage(_bullet.Power);
                objectStack.Enqueue(bulletView);
            }
           
        }

        public BulletView GetObject()
        {
            if (objectStack.Count == 0)
            {
                CreateObject();
            }
            BulletView bullet = objectStack.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.gameObject.transform.position = perent.position;
            return bullet;
        }

        public void PutObject(BulletView currentObject)
        {
            currentObject.gameObject.SetActive(false);
            objectStack.Enqueue(currentObject);
        }
        private void CreateObject()
        {
            //Если стек пустой - создаем  объект
            GameObject newObject = Object.Instantiate(_objectPrefab, perent.position, Quaternion.identity) as GameObject;
            BulletView bulletView = newObject.GetComponent<BulletView>();
            bulletView.SetDamage(_bullet.Power);
            objectStack.Enqueue(bulletView);
        }
    }
}
