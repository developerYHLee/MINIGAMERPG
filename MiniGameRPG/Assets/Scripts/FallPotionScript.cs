using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPotionScript : FallObjectScript
{
    public static int CountPotion = 0;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AvoidPoopPlayer"))
        {
            CountPotion++;
            gameObject.SetActive(false);
        }
    }
}
