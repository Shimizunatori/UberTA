using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ResultManager : MonoBehaviour
{
    [SerializeField] 
    GameObject resultPanel;
    [SerializeField]
    Text MyTimeText;
    [SerializeField]
    Text[] topRankTexts;
    [SerializeField]
    Color defaultColor = Color.white;
    [SerializeField]
    Color highlightColor = Color.yellow;
    [SerializeField]
    PlayerController player;

    private List<float> bestTimes = new List<float>();

    public int touchCount;

    public void ShowResult(float currentTime)
    {
        player.GetComponent<PlayerController>()._clearFlag = true;
        resultPanel.SetActive(true);

        bestTimes.Add(currentTime);
        bestTimes = bestTimes.OrderBy(t => t).Take(3).ToList();

        bool isTop3 = bestTimes.Contains(currentTime);

        MyTimeText.text = $"Your Time: {currentTime:F2}s";

        for (int i = 0; i < topRankTexts.Length; i++)
        {
            if (i < bestTimes.Count)
            {
                topRankTexts[i].text = $"{i + 1}. {bestTimes[i]:F2}s";
            }
            else
            {
                topRankTexts[i].text = $"{i + 1}. ---";
            }
        }

        if (isTop3)
        {
            StartCoroutine(BlinkText(MyTimeText));
        }
    }

    System.Collections.IEnumerator BlinkText(Text text)
    {
        while (true)
        {
            text.color = highlightColor;
            yield return new WaitForSeconds(0.3f);
            text.color = defaultColor;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
