using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour {
    private bool selected = false;
    [SerializeField] private GameObject selectedIcon;
    private GameObject selectionOnIcon;
    private ItemsPlacer universalSelection;

    private void Awake() {
        universalSelection = transform.parent.GetComponent<ItemsPlacer>();
    }

    private void OnMouseDown() {
        if (!selected) {
            SelectCurrentObject();
            universalSelection.SetSelectedObject(this.gameObject);
        } else {
            DeselectCurrentObject();
        }
    }

    public bool isSelected() {
        return selected;
    }

    public void SelectCurrentObject() {
        selectionOnIcon = Instantiate(selectedIcon, transform);
        selected = true;
    }

    public void DeselectCurrentObject() {
        Destroy(selectionOnIcon);
        selected = false;
    }
}
