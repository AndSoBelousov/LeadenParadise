using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeadenParadise
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private float maxDistance = 30f;

        private void OnDrawGizmos()
        {
            Vector3 direction = transform.forward;

            // Переменная для хранения информации о столкновении
            RaycastHit hit;

            // Бросаем луч
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                // Если луч что-то задевает
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.3f);
            }
            else
            {
                // Если луч ничего не задевает в пределах maxDistance
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + direction * maxDistance);
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
        }
    }
}

