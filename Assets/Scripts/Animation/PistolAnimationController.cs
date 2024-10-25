using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAnimationController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem muzzleFlashEffect;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void TriggerOfTheShotAnim()
    {
        _anim.SetTrigger("ShotTrigger");
    }
    public void TriggerMuzzleFlash()
    {
        if (muzzleFlashEffect != null)
        {
            muzzleFlashEffect.Play();
        }
    }
}
