using UnityEngine;
using ZTH.Unity.Tool;

public class BattleField : MonoSingleton<BattleField>
{
    public void Init()
    {
        Pieces = new BattlePiece[GridMapper.GridSize.y, GridMapper.GridSize.x];

        var center = GridMapper.GridSize / 2;
        for (var y = 0; y < GridMapper.GridSize.y; y++)
        {
            for (var x = 0; x < GridMapper.GridSize.x; x++)
            {
                var index = new Vector2Int(x, y);
                var position = GridMapper.Index2WorldPosition(index);
                if (index == center)
                {
                    var piece = CreatePiece(BattlePieceType.Player);
                    piece.transform.position = position;
                    Pieces[y, x] = piece;
                }
                else
                {
                    FillPiece(index);
                }
            }
        }
    }

    public void MovePiece(Vector2Int from, Vector2Int to)
    {
        var piece = Pieces[from.y, from.x];
        var position = GridMapper.Index2WorldPosition(to);
        piece.transform.position = position;
        Pieces[to.y, to.x] = piece;
    }

    public void FillPiece(Vector2Int index)
    {
        var piece = CreatePiece(BattlePieceType.Enemy);
        piece.transform.position = GridMapper.Index2WorldPosition(index);
        Pieces[index.y, index.x] = piece;
    }

    private BattlePiece CreatePiece(BattlePieceType type)
    {
        PieceGenerationId++;
        return BattlePiece.Create(type);
    }

    public GridMapper GridMapper => transform.FindComponent(ref gridMapper); private GridMapper gridMapper;

    public int PieceGenerationId { get; private set; }
    public BattlePiece[,] Pieces { get; private set; }
}
