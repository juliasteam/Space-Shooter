using UnityEngine;

namespace Shooter
{
    public class BulletView : MonoBehaviour
    {
        private int damage;
        private Rigidbody2D rb;
        private BoxCollider2D collider;
        private SpriteRenderer spriteRendere;
        private Sprite image;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<BoxCollider2D>();
            spriteRendere = GetComponent<SpriteRenderer>();
            image = spriteRendere.sprite;
        }
        public BulletView(int _damage)
        {
            damage = _damage;
        }

        public void Shot(int bulletSortingOrder, float bulletSpeed)
        {
            if (bulletSortingOrder != 0)
            {
                spriteRendere.sortingOrder = bulletSortingOrder;
            }
            spriteRendere.sprite = image;
            collider.enabled = true;
            rb.velocity = bulletSpeed * (Vector2.up);
           
        }
        public int GetDamage()
        {
            return damage;
        }
        public void SetDamage(int _damage)
        {
            damage = _damage;
        }
        public void DestroyBullet()
        {
            collider.enabled = false;
            spriteRendere.sprite = null;
        }
       
        private void OnBecameInvisible()
        {
            BulletPool.PutBullet(this);
        }


    }
}
