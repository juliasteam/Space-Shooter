using UnityEngine;

namespace Shooter
{
	public class InputController : MonoBehaviour 
	{
        private float move;
        [SerializeField]
        private ShipView player;

        void FixedUpdate () 
		{
            move = Input.GetAxis("Horizontal");
            if (move != 0)
            {
                player.Move(move);
            }
            if (Input.GetKey(KeyCode.Space))
			{
                player.Fire();
            }
		}
	}

}
