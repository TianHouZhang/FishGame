using UnityEngine;
using ZTH.Unity.Tool;

public class EnemyPiece : MonoBehaviour, IBattlePiece
{
    public void OnContact(IBattlePiece other)
    {
        Debug.Log(other);
    }

    private BattlePiece BasePiece => transform.FindComponent(ref basePiece); private BattlePiece basePiece;
}
