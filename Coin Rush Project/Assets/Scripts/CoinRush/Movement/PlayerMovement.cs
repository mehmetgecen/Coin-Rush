using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using CoinRush.Control;
using AnimatorController = CoinRush.Control.AnimatorController;

namespace CoinRush.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private DynamicJoystick _joystick;
        [SerializeField] private AnimatorController _animatorController;
    
        [SerializeField] private float _movementSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
    
        private Rigidbody _rigidbody;
        private Vector3 _movementVector;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    
        void Update()
        {
            Move();
        }

        private void Move()
        {
            _movementVector = Vector3.zero;
            _movementVector.x = _joystick.Horizontal * _movementSpeed * Time.deltaTime;
            _movementVector.z = _joystick.Vertical * _movementSpeed * Time.deltaTime;

            if (_joystick.Horizontal !=0 || _joystick.Vertical != 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, _movementVector, _rotationSpeed * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(direction);
            
                _animatorController.PlayRunAnimation();
            }
        
            else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
            {
                _animatorController.PlayIdleAnimation();
            }
        
            _rigidbody.MovePosition(_rigidbody.position + _movementVector);
        
        
        }
    
    
    }
}


