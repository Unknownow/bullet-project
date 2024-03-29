﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour {

    //[SerializeField]
    //protected float _initAngle;

    [SerializeField]
    protected CenterBodypartController _centerBody;

    protected float _upperAngle;
    protected float _lowerAngle;
    protected HingeJoint2D _bodyJoint;
    protected Rigidbody2D _rb2d;

	void Start () {
        _bodyJoint = gameObject.GetComponent<HingeJoint2D>();
        _rb2d = gameObject.GetComponent<Rigidbody2D>();

        if (!_bodyJoint)
            return;
        _upperAngle = _bodyJoint.limits.max;
        _lowerAngle = _bodyJoint.limits.min;
        ChangeJointLimitBaseOnRotation();
        //SetJointLimit(_initAngle, _initAngle);

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Terrain"))
        {
            if(_rb2d.velocity.magnitude >= _centerBody.GetMinVelToDie())
            {
                _centerBody.OnCharacterDie(transform.parent);
            }
        }
    }

    protected void SetJointLimit(float upper, float lower)
    {
        JointAngleLimits2D temp = new JointAngleLimits2D
        {
            max = upper,
            min = lower
        };
        _bodyJoint.limits = temp;
    }

    protected void ChangeJointLimitBaseOnRotation()
    {
        float angleRotation = transform.rotation.eulerAngles.z;
        if (angleRotation > 180)
            angleRotation -= 360;
        //_initAngle += angleRotation;
        _upperAngle += angleRotation;
        _lowerAngle += angleRotation;
    }

    public void ReturnJointToNormalState() {
        if (_bodyJoint != null)
            SetJointLimit(_upperAngle, _lowerAngle);
    }

    public CenterBodypartController GetCenterBodyPart()
    {
        return _centerBody;
    }

    public void ApplyForceToThisBodyPart(Vector2 direction, float force)
    {
        _rb2d.AddForce(direction * force);
    }
}


