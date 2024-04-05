using Sirenix.OdinInspector;
using System.IO;
using UnityEngine;
using ZTH.Unity.Tool;

public class Controller : MonoSingleton<Controller>
{
    private void Start()
    {
        for (var i = 0; i < count; i++)
        {
            var prefabFilename = Path.Combine(Controller.I.FishPrefabPath, "TestFish");
            var fishPrefab = prefabFilename.GetResource<Fish>();
            var fish = Instantiate(fishPrefab);
            fish.transform.position = FishTank.I.transform.position;
            fish.Init();
        }
    }

    [SerializeField] private int count = 1;

    [FoldoutGroup("Fish")][SerializeField] private string fishPrefabPath; public string FishPrefabPath => fishPrefabPath;
}
