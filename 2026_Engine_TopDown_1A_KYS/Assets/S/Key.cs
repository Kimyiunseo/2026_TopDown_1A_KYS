using UnityEngine;

public class Key : MonoBehaviour
{
    // 열쇠가 플레이어와 부딪혔을 때 실행되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 대상이 플레이어인지 확인 (PlayerController 컴포넌트가 있는지 체크)
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.GetKey(); // 플레이어에게 열쇠를 얻었다고 신호를 보냄
            Debug.Log("열쇠 획득! (열쇠 스크립트에서 처리됨)");
            Destroy(gameObject); // 열쇠 오브젝트 자신을 삭제
        }
    }
}