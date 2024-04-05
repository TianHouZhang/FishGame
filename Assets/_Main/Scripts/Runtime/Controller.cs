using Sirenix.OdinInspector;
using UnityEngine;
using ZTH.Unity.Tool;

public class Controller : MonoSingleton<Controller>
{
    private void Start()
    {
        FishTank.I.Init();
    }
    [FoldoutGroup("Fish")][SerializeField] private string fishPrefabPath; public string FishPrefabPath => fishPrefabPath;
}
