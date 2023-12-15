using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsPlacer : MonoBehaviour {
    private GameObject selectedObject = null;

    public void SetSelectedObject(GameObject item) {
        selectedObject = item;
        DisableOtherSelections();
    }

    public GameObject GetSelectedObject() {
        return selectedObject;
    }

    private void DisableOtherSelections() {
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++) {
            Transform child = transform.GetChild(childIndex);
            SelectObject childSelectionScript = child.GetComponent<SelectObject>();

            if (child != selectedObject.transform && childSelectionScript.isSelected()) {
                childSelectionScript.DeselectCurrentObject();
            }
        }
    }
}
