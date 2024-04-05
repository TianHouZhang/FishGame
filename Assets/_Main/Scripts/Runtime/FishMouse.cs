using UnityEngine;
using ZTH.Unity.Tool;

public class FishMouse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Owner.State != FishState.Follow) return;
        if (!collision.TryGetComponent<FishFood>(out var food)) return;

        food.Remove();
    }

    private Fish Owner => transform.parent.FindComponent(ref owner); private Fish owner;
}
