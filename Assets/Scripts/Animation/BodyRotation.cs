using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace LeadenParadise.Animation
{
    public class BodyRotation : MonoBehaviour
    {
        private InputHandler _input;
        public Transform target; // Цель, к которой персонаж должен смотреть
        public float maxBodyRotationAngle = 30f; // Максимальный угол поворота корпуса
        public Animator animator; // Ссылка на компонент аниматора
        public float lookAtWeight = 1.0f; // Влияние IK на взгляд
        public float bodyWeight = 0.5f; // Влияние IK на корпус
        public float headWeight = 1.0f; // Влияние IK на голову
        public float handWeight = 1.0f; // Влияние IK на руки
        public float handPositionWeight = 0.5f; // Вес позиции рук

        

        private void Awake()
        {
            _input = FindAnyObjectByType<InputHandler>();
        }

        void Update()
        {
            UpdateLookTargetPosition();
        }

        private void UpdateLookTargetPosition()
        {
            // Преобразуем позицию мыши в мировые координаты
            Ray ray = Camera.main.ScreenPointToRay(_input.LookInput);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                target.position = hitPoint;
            }
        }

        void OnAnimatorIK(int layerIndex)
        {

            if (animator == null || target == null)
                return;

            // Получаем направление на цель, не учитывая высоту
            Vector3 targetDirection = target.position - animator.rootPosition;
            targetDirection.y = 0;

            // Рассчитываем желаемое вращение корпуса
            Quaternion targetBodyRotation = Quaternion.LookRotation(targetDirection);

            // Определяем текущий угол корпуса
            float bodyAngle = Quaternion.Angle(animator.bodyRotation, targetBodyRotation);

            if (bodyAngle > maxBodyRotationAngle)
            {
                // Ограничиваем угол поворота корпуса
                targetBodyRotation = Quaternion.RotateTowards(animator.bodyRotation, targetBodyRotation, maxBodyRotationAngle);
            }

            // Устанавливаем вес IK для головы и корпуса
            animator.SetLookAtWeight(lookAtWeight, bodyWeight, headWeight);

            // Устанавливаем поворот корпуса
            //animator.bodyRotation = Quaternion.Slerp(animator.bodyRotation, targetBodyRotation, Time.deltaTime * 5f);

            // Указываем позицию взгляда
            animator.SetLookAtPosition(target.position);


            SetHandIK(AvatarIKGoal.RightHand);
            SetHandIK(AvatarIKGoal.RightHand);
        }

        private void SetHandIK(AvatarIKGoal hand)
        {
            if (animator == null || target == null)
                return;

            Vector3 handPosition = target.position;
            animator.SetIKPositionWeight(hand, handPositionWeight);
            animator.SetIKRotationWeight(hand, handWeight);
            animator.SetIKPosition(hand, handPosition);
            animator.SetIKRotation(hand, Quaternion.LookRotation(handPosition - animator.GetIKPosition(hand)));
        }






    }
}
