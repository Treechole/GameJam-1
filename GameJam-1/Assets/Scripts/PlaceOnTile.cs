using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnTile : MonoBehaviour {
    private GameObject itemsAvailable;
    private ItemsPlacer selectItems;

    private void Awake() {
        itemsAvailable = GameObject.Find("Place Items");
        selectItems = itemsAvailable.GetComponent<ItemsPlacer>();
    }

    private void OnMouseDown() {
        GameObject objectToBePlaced = selectItems.GetSelectedObject();
        if (objectToBePlaced != null) {
            GameObject wallToBePlaced = objectToBePlaced.transform.GetChild(0).gameObject;
            GameObject wallPlaced = Instantiate(wallToBePlaced);
            wallPlaced.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
