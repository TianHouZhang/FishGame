using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTH.Unity.Tool;

public class FishFood : MonoBehaviour
{
    public void Remove()
    {
        Destroy(gameObject);
    }

    public Rigidbody2D Rigidbody2D => transform.FindComponent(ref rigidbody2D); private new Rigidbody2D rigidbody2D;
}
