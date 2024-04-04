using System;
using System.IO;
using UnityEngine;
using ZTH.Unity.Tool;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BattlePiece : MonoBehaviour
{
    public static BattlePiece Create(BattlePieceType type)
    {
        var typeName = Enum.GetName(typeof(BattlePieceType), type);
        var filename = Path.Combine(Controller.I.BattlePiecePrefabPath, typeName);
        var prefab = filename.GetResource<BattlePiece>();
        var piece = Instantiate(prefab, BattleField.I.transform);
        piece.Id = BattleField.I.PieceGenerationId;
        piece.Type = type;
        return piece;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    private void OnMouseUpAsButton()
    {
        var playerIndex = BattleField.I.GridMapper.WorldPosition2Index(PlayerPiece.I.transform.position);
        var selfIndex = BattleField.I.GridMapper.WorldPosition2Index(transform.position);
        if (selfIndex != playerIndex + Vector2.left &&
            selfIndex != playerIndex + Vector2.right &&
            selfIndex != playerIndex + Vector2.up &&
            selfIndex != playerIndex + Vector2.down) return;

        BattleField.I.MovePiece(playerIndex, selfIndex);
        PlayerPiece.I.OnContact(Owner);
        Owner.OnContact(PlayerPiece.I);

        var playerDirection = selfIndex - playerIndex;
        var moveIndex = playerIndex - playerDirection;
        for (; BattleField.I.GridMapper.IsIn(moveIndex); moveIndex -= playerDirection)
        {
            BattleField.I.MovePiece(moveIndex, moveIndex + playerDirection);
        }

        var fillIndex = moveIndex + playerDirection;
        BattleField.I.FillPiece(fillIndex);

        Remove();
    }

    private void OnDrawGizmos()
    {
        var style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.alignment = TextAnchor.MiddleCenter;

#if UNITY_EDITOR
        var labelPosition = transform.position;
        var labelText = $"{Id}";
        Handles.Label(labelPosition, labelText, style);
#endif
    }

    public int Id { get; private set; }
    public BattlePieceType Type { get; private set; }

    public IBattlePiece Owner => transform.FindComponent(ref owner); private IBattlePiece owner;
}
