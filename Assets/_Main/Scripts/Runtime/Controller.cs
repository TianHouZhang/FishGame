using Sirenix.OdinInspector;
using UnityEngine;
using ZTH.Unity.Tool;

public class Controller : MonoSingleton<Controller>
{
    private void Start()
    {
        BattleField.I.Init();
    }

    [FoldoutGroup("Battle Piece")][SerializeField] private string battlePiecePrefabPath; public string BattlePiecePrefabPath => battlePiecePrefabPath;
}
