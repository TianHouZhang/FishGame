using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTH.Unity.Tool;

public class PlayerPiece : MonoSingleton<PlayerPiece>, IBattlePiece
{
    public void OnContact(IBattlePiece other)
    {
    }
}
