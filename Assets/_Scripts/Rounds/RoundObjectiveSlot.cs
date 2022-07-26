using UnityEngine;
using TMPro;
using System.Text;

public class RoundObjectiveSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text objectiveCountText;
    [SerializeField] private string objectiveName;

    public void SetObjectiveText(int count)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"  x{count} " + objectiveName);

        objectiveCountText.text = sb.ToString();
    }
}
