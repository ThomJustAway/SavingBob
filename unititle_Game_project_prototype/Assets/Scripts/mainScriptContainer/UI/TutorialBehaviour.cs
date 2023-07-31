using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{
  
    [SerializeField] private TypeOfTutorial type = TypeOfTutorial.Tutorial;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextData[] textData;
    [SerializeField] private Button forwardButton;
    [SerializeField] private Button backwardButton;


    private int messageIndex = 0; // the current tutorial message
    private enum TypeOfTutorial{
        Tutorial,
        Tips
    }

    void Start()
    {
        //setting up the tutorial
        title.text = type.ToString();
        SlotMessage(messageIndex);
        forwardButton.onClick.AddListener(IncrementMessageIndex);
        backwardButton.onClick.AddListener(DecrementMessageIndex);
        forwardButton.gameObject.SetActive(true);
        backwardButton.gameObject.SetActive(false);
    }

    private void SlotMessage(int index)
    {
        var data = textData[messageIndex];
        if (data.imageToShow != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = data.imageToShow;
            image.preserveAspect = true;
        }
        else
        {
            //remove the image component
            image.gameObject.SetActive(false) ;
        }
        textComponent.text = data.textToShow;
    } // meant to change the message on the tutorial

    public void IncrementMessageIndex()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        messageIndex++;
        SlotMessage(messageIndex);
        if (messageIndex == textData.Length - 1)
        {
            forwardButton.gameObject.SetActive(false);
        }//remove forwardbutton if it reaches the end of the data.
        else
        {
            backwardButton.gameObject.SetActive(true);
        }
    }

    public void DecrementMessageIndex()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        messageIndex--;
        SlotMessage(messageIndex);
        if (messageIndex == 0)
        {
            backwardButton.gameObject.SetActive(false);
        }// show no option to go to previous instructions
        else
        {
            forwardButton.gameObject.SetActive(true);

        }
    }  //abit annoying that both increment and decrement message index are almost indentical. But This has to do for now.

    public void CloseWindow()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public struct TextData
{
    public Sprite imageToShow;
    [TextArea(3, 10)]
    public string textToShow; 
}
