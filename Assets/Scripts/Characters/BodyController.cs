﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
    public void ChangeCharacterToDynamic(Transform body)
    {
        for (int i = 0; i < body.childCount; i++)
        {
            Transform temp = body.GetChild(i);
            if (temp.GetComponent<BodyPartController>() != null)
            {
                temp.GetComponent<BodyPartController>().ReturnJointToNormalState();
            }
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ChangeCharacterToDynamic(temp);
        }
    }
}
