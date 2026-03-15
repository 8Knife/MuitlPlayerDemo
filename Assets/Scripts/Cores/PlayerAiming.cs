using System;
using UnityEngine;

namespace Cores
{
    public class PlayerAiming : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private InputReader inputReader;


        private void LateUpdate()
        {
            Vector3 mousePosition = inputReader.MousePosition;
            Vector3 aimPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            playerTransform.up = new Vector2(aimPosition.x - playerTransform.position.x,
                aimPosition.y - playerTransform.position.y);
        }
    }
}