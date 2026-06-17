using UnityEngine;
using UnityEngine.SceneManagement; // 스테이지(씬) 전환을 위한 유니티 엔진 기능

public class Door : MonoBehaviour
{
    // 무언가 문에 설치된 Trigger 영역에 들어오면 무조건 실행되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 콘솔 창에 무언가 부딪혔다는 사실을 출력 (테스트용)
        Debug.Log("문(Door)에 무언가 부딪혔습니다! 부딪힌 물체 이름: " + collision.name);

        // 부딪힌 물체의 이름이나 컴포넌트에 관계없이, 문에 닿기만 하면 바로 다음 스테이지 로드!
        // 씬 이름이 숫자 "2"로 저장되어 있으므로 문장 "2"를 넣어 로드합니다.
        SceneManager.LoadScene("2");
    }
}