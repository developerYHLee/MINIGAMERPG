using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScoreScript : FallObjectScript
{
    public static int CountScore = 0;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AvoidPoopPlayer"))
        {
            CountScore++;
            gameObject.SetActive(false);
        }
    }
}
