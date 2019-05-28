using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shooter
{
    public class SimpleObjectPool
    {
        public GameObject _objectPrefab;
        private AsteroidData _asteroid;
        private Stack<AsteroidController> objectStack;
        private Vector2 _maxPosition;

        public SimpleObjectPool(GameObject objectPrefab, int sizePool, AsteroidData asteroid, Vector2 maxPosition)
        {
            Vector3 position;
            _maxPosition = maxPosition;
            objectStack = new Stack<AsteroidController>();
            _objectPrefab = objectPrefab;
            _objectPrefab.SetActive(false);
            _asteroid = asteroid;
            for (int i = 0; i < sizePool; i++)
            {
                position = new Vector3(Random.Range(-maxPosition.x, maxPosition.x), maxPosition.y, 0);
                GameObject newObject = Object.Instantiate(_objectPrefab, position, Quaternion.identity) as GameObject;
                objectStack.Push(new AsteroidController(new AsteroidData(asteroid.HP, asteroid.Speed, asteroid.Damage, asteroid.Type), newObject.GetComponent<AsteroidView>()));
            }
        }

        public AsteroidController GetObject()
        {
            if (objectStack.Count == 0)
            {
                CreateObject();
            }
            objectStack.Peek().Reset();
            objectStack.Peek().OnObject();
            return objectStack.Pop();
        }

        public void PutObject(AsteroidController currentObject)
        {
            objectStack.Push(currentObject);
            Vector3 position = new Vector3(Random.Range(-_maxPosition.x, _maxPosition.x), _maxPosition.y, 0);
            objectStack.Peek().OffObject(position);
        }
        private void CreateObject()
        {
            //Если стек пустой - создаем объект
            Vector3 position = new Vector3(Random.Range(-_maxPosition.x, _maxPosition.x), _maxPosition.y, 0);
            GameObject newObject = Object.Instantiate(_objectPrefab, position, Quaternion.identity) as GameObject;
            objectStack.Push(new AsteroidController(new AsteroidData(_asteroid.HP, _asteroid.Speed, _asteroid.Damage, _asteroid.Type), newObject.GetComponent<AsteroidView>())); 
        }
    }
}
