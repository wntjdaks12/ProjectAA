using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotModel
{
    public IconInfo iconInfo;
    public DBItemInfo itemInfo;
}

public class InventorySlotPresenter
{
    private InventorySlotModel model;
    private IInventorySlotView view;

    public InventorySlotPresenter(IInventorySlotView view)
    {
        this.view = view;
        model = new InventorySlotModel();
    }

    public void Init(int id)
    {
        model.iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), id);
        model.itemInfo = GameApplication.Instance.PlayerManager.DBItemInfos.Find(x => x.id == id);

        view.UpdateUI(model);
    }
}

public interface IInventorySlotView
{
    public void UpdateUI(InventorySlotModel model);
}

public class InventorySlot : MonoBehaviour, IInventorySlotView
{
    [SerializeField] private Image iconImg;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private Button slotBtn;

    private InventorySlotPresenter presenter;

    public void Init(int id)
    {
        presenter ??= new InventorySlotPresenter(this);
        presenter.Init(id);

        slotBtn.onClick.RemoveAllListeners();
        slotBtn.onClick.AddListener(() =>
        {
            var textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), id);
            var tooltipBox = Instantiate(Resources.Load<TooltipBox>("Prefabs/UI/TooltipBox/TooltipBox"));

            tooltipBox.Init(textInfo, Input.mousePosition);
        });
    }

    public void UpdateUI(InventorySlotModel model)
    {
        iconImg.sprite = Resources.Load<Sprite>(model.iconInfo.Path);
        numText.text = model.itemInfo.value.ToString();
    }
}
