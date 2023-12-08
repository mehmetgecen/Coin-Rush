using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinRush.Control
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;
    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayRunAnimation()
        {
            _animator.Play("MainCharacterRun");
        }
    
        public void PlayIdleAnimation()
        {
            _animator.Play("MainCharacterIdle");
        }
    }
}



