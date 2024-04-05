using System.Collections;
using UnityEngine;
using ZTH.Unity.Tool;

public class Fish : MonoBehaviour
{
    public void Init()
    {
        if (Random.Range(0, 2) == 0)
        {
            ChangeHDirection();
        }

        StartRandomChangeHDirection();

        if (Random.Range(0, 2) == 0)
        {
            ChangeVDirection();
        }

        StartRandomChangeVDirection();
    }

    #region 水平方向运动

    private void StartRandomChangeHDirection()
    {
        hChangeCoroutine = StartCoroutine(OnRandomChangeHDirection());
    }

    private IEnumerator OnRandomChangeHDirection()
    {
        while (true)
        {
            var interval = Random.Range(minHDirectionChangeInterval, maxHDirectionChangeInterval);
            yield return new WaitForSeconds(interval);
            ChangeHDirection();
        }
    }

    private void ChangeHDirection()
    {
        IsRightDirection = !IsRightDirection;
    }

    #endregion

    #region 垂直方向运动

    private void StartRandomChangeVDirection()
    {
        vChangeCoroutine = StartCoroutine(OnRandomChangeVDirection());
    }

    private IEnumerator OnRandomChangeVDirection()
    {
        yield return new WaitForSeconds(minVDirectionChangeInterval);

        while (true)
        {
            var interval = Random.Range(minVDirectionChangeInterval, maxVDirectionChangeInterval);
            yield return new WaitForSeconds(interval);
            ChangeVDirection();
        }
    }

    private void ChangeVDirection()
    {
        upDirectionIndex = (upDirectionIndex + 1) % UP_DIRECTIONS.Length;
    }

    #endregion

    private void Update()
    {
        var position = transform.position;

        var hMoveSpeed = Random.Range(minHMoveSpeed, maxHMoveSpeed);
        var vMoveSpeed = Random.Range(minVMoveSpeed, maxVMoveSpeed);

        position.x += (IsRightDirection ? 1 : -1) * Time.deltaTime * hMoveSpeed;
        position.y += UP_DIRECTIONS[upDirectionIndex] * Time.deltaTime * vMoveSpeed;
        position.z = -position.y;

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Left")
        {
            IsRightDirection = true;
            StopCoroutine(hChangeCoroutine);
            StartRandomChangeHDirection();
        }
        else
        if (other.name == "Right")
        {
            IsRightDirection = false;
            StopCoroutine(hChangeCoroutine);
            StartRandomChangeHDirection();
        }
        else
        if (other.name == "Top")
        {
            upDirectionIndex = 0;
            StopCoroutine(vChangeCoroutine);
            StartRandomChangeVDirection();
        }
        else
        if (other.name == "Bottom")
        {
            upDirectionIndex = 1;
            StopCoroutine(vChangeCoroutine);
            StartRandomChangeVDirection();
        }
    }

    [SerializeField] private float minHMoveSpeed = 1;
    [SerializeField] private float maxHMoveSpeed = 3;
    [SerializeField] private float minVMoveSpeed = 1;
    [SerializeField] private float maxVMoveSpeed = 3;
    [SerializeField] private float minHDirectionChangeInterval = 1;
    [SerializeField] private float maxHDirectionChangeInterval = 3;
    [SerializeField] private float minVDirectionChangeInterval = 1;
    [SerializeField] private float maxVDirectionChangeInterval = 3;

    private bool IsRightDirection
    {
        get => transform.eulerAngles.y == 0;
        set => transform.eulerAngles = transform.eulerAngles.SetY(value ? 0 : 180);
    }

    private int upDirectionIndex;
    private Coroutine hChangeCoroutine;
    private Coroutine vChangeCoroutine;

    private readonly int[] UP_DIRECTIONS = new int[] { -1, 0, 1 };
}