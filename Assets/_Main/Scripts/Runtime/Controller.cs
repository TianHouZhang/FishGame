using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTH.Unity.Tool;

public class Controller : MonoSingleton<Controller>
{
    [SerializeField] private string test; public string Test => test;
}
