using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public int _layer;
        public string _sortingLayer;

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = _layer;
            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = _sortingLayer;   
        }
    }
}
