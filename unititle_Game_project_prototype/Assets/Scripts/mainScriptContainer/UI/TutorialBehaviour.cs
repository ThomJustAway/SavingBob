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
        //setting up the tutorial by setting the values
        title.text = type.ToString();
        SlotMessage();
        forwardButton.onClick.AddListener(IncrementMessageIndex);
        backwardButton.onClick.AddListener(DecrementMessageIndex);

        //show the forward button as the tutorial starts at message Index 0
        forwardButton.gameObject.SetActive(true);
        backwardButton.gameObject.SetActive(false);
    }

    private void SlotMessage()
    {
        //change the message to the next message
        var data = textData[messageIndex];
        if (data.imageToShow != null)
        {//if the data has a image to show, then show the image
            image.gameObject.SetActive(true);
            image.sprite = data.imageToShow;
            image.preserveAspect = true;
        }
        else
        {
            //Dont show if there is no image to show
            image.gameObject.SetActive(false) ;
        }
        textComponent.text = data.textToShow;
    } // meant to change the message on the tutorial

    public void IncrementMessageIndex()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        messageIndex++;
        SlotMessage();
        if (messageIndex == textData.Length - 1)
        { 
            forwardButton.gameObject.SetActive(false);
        }//remove forwardbutton if it reaches the end of the data.
        else
        {
            backwardButton.gameObject.SetActive(true);
        //will always show the back button to show that player can return to the previous message index
        }
    }

    public void DecrementMessageIndex()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        messageIndex--;
        SlotMessage();
        if (messageIndex == 0)
        {
            backwardButton.gameObject.SetActive(false);
        }// show no option to go to previous instructions
        else
        {
            forwardButton.gameObject.SetActive(true);

        }
    }  //abit annoying that both increment and decrement message index function are almost indentical. But This has to do for now.

    public void CloseWindow()
    {
        //make the tutorial not visible by not showing anything
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public struct TextData
{
    //data to contain the text message and image
    public Sprite imageToShow;
    [TextArea(3, 10)]
    public string textToShow; 
}
