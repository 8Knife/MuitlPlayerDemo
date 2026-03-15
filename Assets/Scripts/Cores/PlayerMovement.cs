using Unity.Netcode;
using UnityEngine;

namespace Cores
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField]private InputReader inputReader;
        [SerializeField]private Transform playerTransform;
        [SerializeField]private Rigidbody2D playerRigidbody2D;

        [Header("Settings")]
        [SerializeField]private float moveSpeed = 4;
        [SerializeField] private float turnRotate = 30;

        private Vector2 _moveValue;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            inputReader.MoveEvent += HandleMovement;
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner) return;

            inputReader.MoveEvent -= HandleMovement;
        }

        private void Update()
        {
            if (!IsOwner) return;

            playerTransform.Rotate(0, 0,
                _moveValue.x * -turnRotate * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;

            playerRigidbody2D.velocity = playerTransform.up * (_moveValue.y * moveSpeed);
        }

        private void HandleMovement(Vector2 moveValue)
        {
            _moveValue = moveValue;
        }
    }
}
