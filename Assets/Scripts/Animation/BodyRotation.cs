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
        public Transform target; // ����, � ������� �������� ������ ��������
        public float maxBodyRotationAngle = 30f; // ������������ ���� �������� �������
        public Animator animator; // ������ �� ��������� ���������
        public float lookAtWeight = 1.0f; // ������� IK �� ������
        public float bodyWeight = 0.5f; // ������� IK �� ������
        public float headWeight = 1.0f; // ������� IK �� ������
        public float handWeight = 1.0f; // ������� IK �� ����
        public float handPositionWeight = 0.5f; // ��� ������� ���

        

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
            // ����������� ������� ���� � ������� ����������
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

            // �������� ����������� �� ����, �� �������� ������
            Vector3 targetDirection = target.position - animator.rootPosition;
            targetDirection.y = 0;

            // ������������ �������� �������� �������
            Quaternion targetBodyRotation = Quaternion.LookRotation(targetDirection);

            // ���������� ������� ���� �������
            float bodyAngle = Quaternion.Angle(animator.bodyRotation, targetBodyRotation);

            if (bodyAngle > maxBodyRotationAngle)
            {
                // ������������ ���� �������� �������
                targetBodyRotation = Quaternion.RotateTowards(animator.bodyRotation, targetBodyRotation, maxBodyRotationAngle);
            }

            // ������������� ��� IK ��� ������ � �������
            animator.SetLookAtWeight(lookAtWeight, bodyWeight, headWeight);

            // ������������� ������� �������
            //animator.bodyRotation = Quaternion.Slerp(animator.bodyRotation, targetBodyRotation, Time.deltaTime * 5f);

            // ��������� ������� �������
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
