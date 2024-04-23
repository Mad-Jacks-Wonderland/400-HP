using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class WeightedPart // this is a code for weighted parts to include in procedural weapon adaptation of attachments
{
    public string partName; // purely cosmetic, and used mainly for testing
    public int weight; // weight for weighted selection
    public GameObject gunPart; //the part itself
}
