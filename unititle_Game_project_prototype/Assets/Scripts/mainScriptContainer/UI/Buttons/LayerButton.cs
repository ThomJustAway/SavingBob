using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LayerButton : CustomButton
{
    [SerializeField] private TextMeshProUGUI buttonText;
    private int controllingLayer;
    private LayerManager layerManager;
    protected override void Start()
    {
        base.Start();
        layerManager = LayerManager.instance;
        if(layerManager.CurrentLayer == controllingLayer)
        {
            image.color = ActivatedColor;
        }
        layerManager.onButtonClick.AddListener(CheckIfChange);
    }

    public void Init(int layer)
    {
        controllingLayer = layer;
        buttonText.text = $"Layer {layer}";
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        CheckIfChange();
    }

    private void CheckIfChange()
    {
        if (layerManager.CurrentLayer == controllingLayer)
        {
            image.color = ActivatedColor;
        }
        else
        {
            image.color = normalColor;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        layerManager.ChangeLayer(controllingLayer);
        image.color = HoverColor;
        layerManager.onButtonClick?.Invoke();
    }


}
