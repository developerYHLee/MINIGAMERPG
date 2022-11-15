using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPoopScript : FallObjectScript
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AvoidPoopPlayer"))
        {
            for (int i = 0; i < 3; i++) _background.GetComponent<PlayAvoidPoopScript>().StopCoroutine(_background.GetComponent<PlayAvoidPoopScript>()._enumerator[i]);
            _background.SetActive(false);
            _result.SetActive(true);

            AvoidPoopCanvasScript avoidPoopCanvasScript = _background.transform.parent.GetComponent<AvoidPoopCanvasScript>();
            int stage = avoidPoopCanvasScript._stage;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            //해당되는 단계의 MiniGameSpot을 비활성화 시킨다.
            gameManager._miniGameSpot[stage].SetActive(false);

            //미니게임 수행 유무 저장
            DataController.Instance.gameData._miniGameIsCleared[stage] = true;

            //결과에 점수를 나타냄
            avoidPoopCanvasScript._resultScoreText.text = ": " + FallScoreScript.CountScore;
            avoidPoopCanvasScript._resultPotionText.text = ": " + FallPotionScript.CountPotion;

            //결과에 적 능력치 감소 정도를 나타냄
            avoidPoopCanvasScript._resultDecreaseHpText.text = "적 HP " + FallScoreScript.CountScore * 10 * (stage + 1) + " 감소";
            avoidPoopCanvasScript._resultDecreaseDamageText.text = "적 Damage " + FallScoreScript.CountScore * 3  * (stage + 1) + " 감소";

            Debug.Log("똥피하기 종료");
        }
    }
}
