using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class GoldUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_goldText;
        [SerializeField] private IntEvent m_goldUIEvent;

        private void OnEnable()
        {
            m_goldUIEvent.AddListener(OnUpdateGoldNumber);
        }

        private void OnDisable()
        {
            m_goldUIEvent.RemoveListener(OnUpdateGoldNumber);
        }

        private void OnUpdateGoldNumber(int value)
        {
            m_goldText.text = value.ToString();
        }
    }
}
