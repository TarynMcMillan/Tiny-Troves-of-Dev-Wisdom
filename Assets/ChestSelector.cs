using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSelector : MonoBehaviour
{
    
    private bool isSelected = false;

    private void OnMouseDown()
    {
        // Handle mouse click
        SelectChest();
    }

    private void OnTouchDown()
    {
        // Handle touchscreen tap
        SelectChest();
    }

    private void SelectChest()
    {
        isSelected = true;
        // You can add visual feedback to indicate that the chest is selected (e.g., highlight it).
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}


