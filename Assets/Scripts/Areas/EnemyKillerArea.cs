using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeamUp.Areas
{
    public class EnemyKillerArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.attachedRigidbody)
            {
                if (collision.attachedRigidbody.TryGetComponent<Enemy>(out var enemy)){
                    
                        enemy.Kill();
                }
            }
        }
    }
}