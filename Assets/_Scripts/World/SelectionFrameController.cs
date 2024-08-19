using System;
using UnityEngine;

public class SelectionFrameController : MonoBehaviour
{
    [SerializeField] private GameObject m_selectionFrame;

    private void Start()
    {
        HideSelection();
    }

    public void ShowSelection()
    {
        m_selectionFrame.SetActive(true);
    }

    public void HideSelection()
    {
        m_selectionFrame.SetActive(false);
    }
}
