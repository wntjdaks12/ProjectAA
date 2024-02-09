using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipBoxModel
{ 
    public TextInfo TextInfo { get; set; }
}

public class TooltipBoxPresenter
{
    private TooltipBoxModel model;
    private TooltipBoxView view;

    public TooltipBoxPresenter(TooltipBoxView view, TextInfo TextInfo)
    {
        this.view = view;
        model = new TooltipBoxModel();
        model.TextInfo = TextInfo;
    }

    public void Init()
    {

        view.UpdateUI(model);
    }
}

public interface TooltipBoxView
{
    public void UpdateUI(TooltipBoxModel model);
}

public class TooltipBox : MonoBehaviour, TooltipBoxView
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public TooltipBoxPresenter presenter;
    public void Init(TextInfo textInfo, Vector3 position)
    {
        presenter = new TooltipBoxPresenter(this, textInfo);
        presenter.Init();

        transform.SetParent(GameObject.Find("StaticOverlayCanvas").transform, false);

        if (position.x - GetComponent<RectTransform>().rect.width < 0)
        {
            transform.position = position;
        }
        else if (position.x + GetComponent<RectTransform>().rect.width > Screen.width)
        {
            transform.position = new Vector3(position.x - GetComponent<RectTransform>().rect.width, position.y, position.z);
        }
        else
        {
            transform.position = position;
        }   
    }

    public void UpdateUI(TooltipBoxModel model)
    {
        nameText.text = model.TextInfo.NameKR;
        descriptionText.text = model.TextInfo.DescriptionKR;

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
}
    