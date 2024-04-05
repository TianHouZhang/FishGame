using UnityEngine;
using ZTH.Unity.Tool;

public class FishFOV : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<FishFood>(out var food)) return;

        if (Owner.HasCourage(food.Rigidbody2D))
            Owner.StartFollowState(food.Rigidbody2D);
        else
            Owner.StartEscapceState(food.Rigidbody2D);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<FishFood>(out var _)) return;

        Owner.StartIdleState();
    }

    private Fish Owner => transform.parent.FindComponent(ref owner); private Fish owner;
}
