using UnityEngine;
using ZTH.Unity.Tool;

public class FishArm : MonoSingleton<FishArm>
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {

        }

        var h = Input.GetAxisRaw("Horizontal");
        FishRod.AddForce(new Vector2(h * moveSpeed, 0));
    }

    [SerializeField] private float moveSpeed = 1;

    public Rigidbody2D FishRod => transform.FindComponent(ref fishRod, "FishRod"); private Rigidbody2D fishRod;
}
