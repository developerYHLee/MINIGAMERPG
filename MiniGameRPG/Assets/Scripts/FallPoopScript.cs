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
            //�ش�Ǵ� �ܰ��� MiniGameSpot�� ��Ȱ��ȭ ��Ų��.
            gameManager._miniGameSpot[stage].SetActive(false);

            //�̴ϰ��� ���� ���� ����
            DataController.Instance.gameData._miniGameIsCleared[stage] = true;

            //����� ������ ��Ÿ��
            avoidPoopCanvasScript._resultScoreText.text = ": " + FallScoreScript.CountScore;
            avoidPoopCanvasScript._resultPotionText.text = ": " + FallPotionScript.CountPotion;

            //����� �� �ɷ�ġ ���� ������ ��Ÿ��
            avoidPoopCanvasScript._resultDecreaseHpText.text = "�� HP " + FallScoreScript.CountScore * 10 * (stage + 1) + " ����";
            avoidPoopCanvasScript._resultDecreaseDamageText.text = "�� Damage " + FallScoreScript.CountScore * 3  * (stage + 1) + " ����";

            Debug.Log("�����ϱ� ����");
        }
    }
}
