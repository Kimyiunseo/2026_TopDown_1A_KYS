using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // --- [이동 및 속도 설정] ---
    public float moveSpeed = 5f;

    // --- [방향별 애니메이션 스프라이트 배열] ---
    public Sprite[] spriteUp;
    public Sprite[] spriteDown;
    public Sprite[] spriteLeft;
    public Sprite[] spriteRight;
    public float frameTime = 0.15f;

    // --- [컴포넌트 및 내부 변수] ---
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 input;
    private Vector2 velocity;
    private Sprite[] currentSprites;
    private int frameIndex = 0;
    private float timer = 0f;

    // --- [게임 플레이 상태 변수] ---
    private bool hasKey = false;       // 내부적으로 열쇠 보유 여부 저장

    // --- [인풋 시스템 이동 입력 함수] ---
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > 0.01f)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0) ChangeSprites(spriteRight);
                else ChangeSprites(spriteLeft);
            }
            else
            {
                if (input.y > 0) ChangeSprites(spriteUp);
                else ChangeSprites(spriteDown);
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentSprites = spriteDown;
        sr.sprite = currentSprites[0];
    }

    private void Update()
    {
        if (input.sqrMagnitude <= 0.01f)
        {
            frameIndex = 0;
            sr.sprite = currentSprites[frameIndex];
            return;
        }

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;
            frameIndex++;

            if (frameIndex >= currentSprites.Length)
                frameIndex = 0;

            sr.sprite = currentSprites[frameIndex];
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void ChangeSprites(Sprite[] newSprites)
    {
        if (currentSprites == newSprites)
            return;

        currentSprites = newSprites;
        frameIndex = 0;
        timer = 0;
        sr.sprite = currentSprites[frameIndex];
    }

    // --- [열쇠/문 스크립트와 소통하기 위한 외부 공개 함수들] ---

    // 열쇠 스크립트가 호출해 줄 함수 (열쇠를 획득 처리)
    public void GetKey()
    {
        hasKey = true;
    }

    // 문 스크립트가 호출해 줄 함수 (열쇠가 있는지 확인하여 참/거짓을 알려줌)
    public bool CheckHasKey()
    {
        return hasKey;
    }
}