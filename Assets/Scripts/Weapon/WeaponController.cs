using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace LeadenParadise
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private float maxDistance = 30f;


        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private GameObject _shellCasing;
        private GameObject[] _pistolClip;// todo


        [SerializeField]
        private Transform _cacheOfCasings;
        [SerializeField]
        private GameObject[] _spentCartridges = new GameObject[15];
        [SerializeField]
        private int _currentSpentCartridg = 0;

        public float moveDuration = 0.5f; // Время перемещения

        private PistolAnimationController _pistolAnimation;

        private void Awake()
        {
            _pistolAnimation  = FindFirstObjectByType<PistolAnimationController>();
            //_cacheOfCasings = FindFirstObjectByType<>();
        }
        private void Start()
        {

            for(int i = 0;  i < _spentCartridges.Length; i++)
            {
                GameObject obj = Instantiate(_shellCasing, transform.position , transform.rotation);
                obj.transform.SetParent(_cacheOfCasings.transform, false);
                obj.SetActive(false);
                _spentCartridges[i] = obj;
            }
        }

        private void CachingOfCasings()
        {
            if (_spentCartridges[_currentSpentCartridg].activeSelf)
            {
                _spentCartridges[_currentSpentCartridg].SetActive(false);
                _spentCartridges[_currentSpentCartridg].transform.position = transform.position;
            }
            _spentCartridges[_currentSpentCartridg].transform.position = transform.position + new Vector3(0f,0.1f,0f);
            _spentCartridges[_currentSpentCartridg].transform.rotation = transform.rotation;
            _spentCartridges[_currentSpentCartridg].SetActive(true);

            _currentSpentCartridg++;
            if (_currentSpentCartridg == _spentCartridges.Length) 
                _currentSpentCartridg = 0;

        }
        private void PistolShotStart()
        {
            _pistolAnimation.TriggerOfTheShotAnim();

            CachingOfCasings();

            Vector3 direction = transform.forward;
            RaycastHit hit; 
            if(Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                StartCoroutine(SphereIndicator(hit.point));

                //GameObject hitObject = hit.transform.gameObject; // получаем объект в который попал луч
                //ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                //if (target != null)
                //{
                //    target.ReactToHit();                //вызов метода при поподании
                //    Debug.Log("Target hit");
                //}
                //else
                //{
                //    StartCoroutine(SphereIndicator(hit.point));
                //}
            }

            //EditorApplication.isPaused = true;
        }

        private IEnumerator SphereIndicator(Vector3 pos) //////// ИМЕНИТЬ!!!! TODO
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
            sphere.transform.position = pos; 
            sphere.transform.localScale= Vector3.one * 0.1f;
            yield return new WaitForSeconds(1);                                
            Destroy(sphere);                                                
        }

        private void OnEnable()
        {
            InputHandler.OnShotPressed += PistolShotStart;
        }
        private void OnDisable()
        {
            InputHandler.OnShotPressed -= PistolShotStart;
        }

        private void OnDrawGizmos()
        {
            Vector3 direction = transform.forward;
            RaycastHit hit;


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

    }
}

