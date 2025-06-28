using System.Collections;
using UnityEngine;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public GameObject cutscenePanel;
    public TextMeshProUGUI dialogueText;

    public string[] lines = {
        "Group Admin: Hey coders, weve got a mission.The Facebook group is being attacked… by actual working code!",
        "Member: Wait, actual working code? In Imphnen? Thats illegal!",
        "Group Admin: If we don't act fast, motivation might spread!",
        "Member: We can't let that happen. Quick someone post a meme about suffering to be Dev"
    };

    public float delayBetweenLines = 3f;
    private int currentLine = 0;

    void Start()
    {
        Time.timeScale = 0f; // Pause game during cutscene
        cutscenePanel.SetActive(true);
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        while (currentLine < lines.Length)
        {
            dialogueText.text = lines[currentLine];
            currentLine++;
            yield return new WaitForSecondsRealtime(delayBetweenLines);
        }

        EndCutscene();
    }

    void EndCutscene()
    {
        cutscenePanel.SetActive(false);
        Time.timeScale = 1f; // Resume game
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            EndCutscene();
        }
    }
}
