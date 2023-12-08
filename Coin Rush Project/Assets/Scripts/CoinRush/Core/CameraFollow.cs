using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    
    private Transform _cameraTransform;
    private Vector3 _offset;

    private void Awake()
    {
        _cameraTransform = this.transform;
        _offset = _cameraTransform.position - _target.position;
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        _cameraTransform.DOMoveX(_target.position.x + _offset.x, _speed * Time.deltaTime, false);
        _cameraTransform.DOMoveZ(_target.position.z + _offset.z, _speed * Time.deltaTime,false);
    }
}
