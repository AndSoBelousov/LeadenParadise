using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LeadenParadise
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private float maxDistance = 30f;
        [SerializeField]
        private GameObject shutter; // затвор 

        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private GameObject _shellCasing;
        [SerializeField]
        private GameObject[] _pistolClip;

        private Transform _shutterPosition;
        private Transform _shutterTargetPosition;

        public float moveDuration = 0.5f; // Время перемещения
        

        private void Start()
        {
            if (shutter != null) _shutterPosition = shutter.transform;
            _shutterTargetPosition = _shutterPosition;
            _shutterTargetPosition.position += new Vector3(0f, 0f, 0.1f);


        }
        private void PistolShotStart()
        {
            GameObject bullet = Instantiate(_bullet);
            bullet.transform.localPosition = transform.position;
            bullet.transform.localRotation = transform.rotation;

            //Instantiate(_bullet,transform.position, transform.localRotation);
            Instantiate(_shellCasing, transform.position + new Vector3(0f,0f,-0.2f), transform.localRotation);
        }
       

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

            }
        }

        private void OnEnable()
        {
            InputHandler.OnShotPressed += PistolShotStart;
        }
        private void OnDisable()
        {
            InputHandler.OnShotPressed -= PistolShotStart;
        }
    }
}

