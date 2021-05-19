using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private Text uiText;
    private string textToWrite;        // 故事文字
    private float timePerCharacter;    // 每秒的文字量
    private int characterIndex;        // 用於計算的文字字數
    private float timer;               // 計時器
    private bool invisiableCharacters; // 

    public void AddWriter(Text uiText, string textToWrite, float timePerCharacter,bool invisiableCharacters)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisiableCharacters = invisiableCharacters;
        characterIndex = 0;
    }

    private void Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);

                if (characterIndex >= textToWrite.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }
    }
}
