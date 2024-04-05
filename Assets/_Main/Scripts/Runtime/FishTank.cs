using System.IO;
using UnityEngine;
using ZTH.Unity.Tool;

public class FishTank : MonoSingleton<FishTank>
{
    public void Init()
    {
        for (var i = 0; i < fishCount; i++)
        {
            var prefabFilename = Path.Combine(Controller.I.FishPrefabPath, "TestFish");
            var fishPrefab = prefabFilename.GetResource<Fish>();
            var fish = Instantiate(fishPrefab, FishManager.I.transform);
            fish.transform.position = FishTank.I.transform.position;
            fish.Init();
        }
    }

    [SerializeField] private int fishCount = 1;
}
