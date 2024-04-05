using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using ZTH.Unity.Tool;

public class Fish : MonoBehaviour
{
    public void Init()
    {
        ChangeMoveSpeed();
        ChangeDirection();
        StartIdleState();
    }

    #region ÏÐÖÃ×´Ì¬

    public void StartIdleState()
    {
        StopState();
        State = FishState.Idle;
        coroutine = StartCoroutine(OnIdleState());
    }

    private IEnumerator OnIdleState()
    {
        while (true)
        {
            var interval = Random.Range(minDirectionChangeInterval, maxDirectionChangeInterval);
            yield return new WaitForSeconds(interval);
            ChangeMoveSpeed();
            ChangeDirection();
        }
    }

    private void ChangeMoveSpeed()
    {
        moveSpeedTo = Random.Range(0, 2) == 0 ? Random.Range(minMoveSpeed, maxMoveSpeed) : 0;
    }

    private void ChangeDirection()
    {
        var radian = Random.Range(0, 360) * Mathf.Deg2Rad;
        var x = Mathf.Cos(radian);
        var y = Mathf.Sin(radian);
        direction = new Vector2(x, y).normalized;
    }

    #endregion

    #region ¸úËæ×´Ì¬

    public void StartFollowState(Rigidbody2D target)
    {
        StopState();
        State = FishState.Follow;
        moveSpeedTo = minMoveSpeed;
        coroutine = StartCoroutine(OnFollowState(target));
    }

    private IEnumerator OnFollowState(Rigidbody2D target)
    {
        while (target != null)
        {
            yield return null;

            if (!HasCourage(target))
            {
                StartEscapceState(target);
                yield break;
            }
            else
            {
                direction = (target.transform.position - transform.position).normalized;
            }
        }

        StartIdleState();
    }

    #endregion

    #region ÌÓÅÜ×´Ì¬

    public void StartEscapceState(Rigidbody2D target)
    {
        StopState();
        State = FishState.Escape;
        moveSpeedTo = maxMoveSpeed;
        coroutine = StartCoroutine(OnEscapeState(target));
    }

    private IEnumerator OnEscapeState(Rigidbody2D target)
    {
        while (target != null)
        {
            yield return null;
            direction = (transform.position - target.transform.position).normalized;
        }

        StartIdleState();
    }

    #endregion

    private void StopState()
    {
        if (coroutine == null) return;

        StopCoroutine(coroutine);
        coroutine = null;
    }

    private void Update()
    {
        switch (State)
        {
            case FishState.Idle: SpriteRenderer.color = Color.white; break;
            case FishState.Follow: SpriteRenderer.color = Color.green; break;
            case FishState.Escape: SpriteRenderer.color = Color.red; break;
        }
    }

    private void FixedUpdate()
    {
        moveSpeed = Mathf.SmoothDamp(moveSpeed, moveSpeedTo, ref moveSpeedVelocity, moveChangeInterval);
        var motion = direction * moveSpeed * Time.deltaTime;
        Rigidbody2D.velocity = motion;
        transform.eulerAngles = transform.eulerAngles.SetY(motion.x >= 0 ? 0 : 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Left")
        {
            direction.x = Mathf.Abs(direction.x);
        }
        else
        if (collision.gameObject.name == "Right")
        {
            direction.x = -Mathf.Abs(direction.x);
        }
        else
        if (collision.gameObject.name == "Top")
        {
            direction.y = -Mathf.Abs(direction.y);
        }
        else
        if (collision.gameObject.name == "Bottom")
        {
            direction.y = Mathf.Abs(direction.y);
        }
    }

    public bool HasCourage(Rigidbody2D target)
    {
        return courage >= target.velocity.magnitude;
    }

    [SerializeField] private float minMoveSpeed = 1;
    [SerializeField] private float maxMoveSpeed = 5;
    [SerializeField] private float moveChangeInterval = 1;
    [SerializeField] private float minDirectionChangeInterval = 1;
    [SerializeField] private float maxDirectionChangeInterval = 3;
    [SerializeField] private float courage = 1;

    [ShowInInspector][ReadOnly] public FishState State { get; private set; }

    public SpriteRenderer SpriteRenderer => transform.FindComponent(ref spriteRenderer); private SpriteRenderer spriteRenderer;
    public Rigidbody2D Rigidbody2D => transform.FindComponent(ref rigidbody2D); private new Rigidbody2D rigidbody2D;

    private Vector2 direction;
    private float moveSpeed;
    private float moveSpeedTo;
    private float moveSpeedVelocity;
    private Coroutine coroutine;
}