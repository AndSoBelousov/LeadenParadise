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

            // ���������� ��� �������� ���������� � ������������
            RaycastHit hit;

            // ������� ���
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                // ���� ��� ���-�� ��������
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.3f);
            }
            else
            {
                // ���� ��� ������ �� �������� � �������� maxDistance
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + direction * maxDistance);
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
        }
    }
}

