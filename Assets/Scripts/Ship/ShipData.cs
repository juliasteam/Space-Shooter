using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Shooter
{
    [Serializable]
    public class ShipData : ISerializable
    {
        public int HP;
        public float Speed;
        public float PositionX;
        public float PositionY;
        public float FireRate;
        public int Score;
       
       
        public ShipData(int hp, float speed, float posX, float posY, float firerate, int score)
        {
            HP = hp;
            Speed = speed;
            PositionX = posX;
            PositionY = posY; 
            FireRate = firerate; 
            Score = score;
        }

        public ShipData()
        {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("HP", this.HP);
            info.AddValue("Speed", this.Speed);
            info.AddValue("PositionX", this.PositionX);
            info.AddValue("PositionY", this.PositionY);
            info.AddValue("FireRate", this.FireRate);
            info.AddValue("Score", this.Score);
        }

        public ShipData(SerializationInfo info, StreamingContext context)
        {
            this.HP = (int)info.GetValue("HP", typeof(int));
            this.Speed = (float)info.GetValue("Speed", typeof(float));
            this.PositionX = (float)info.GetValue("PositionX", typeof(float));
            this.PositionY = (float)info.GetValue("PositionY", typeof(float));
            this.FireRate = (float)info.GetValue("FireRate", typeof(float));
            this.Score = (int)info.GetValue("Score", typeof(int));
        }
    }
}
