using UnityEngine;
using UnityEngine.UI;

public class SubItemSlotModel
{
    public IconInfo iconInfo;
    public TextInfo textInfo;
}

public class SubItemSlotPresenter
{
    public SubItemSlotModel Model { get; private set; }
    private ISubItemSlotView view;

    public SubItemSlotPresenter(ISubItemSlotView view)
    {
        this.view = view;
        Model = new SubItemSlotModel();
    }

    public void Init(int id)
    {
        Model.iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), id);
        Model.textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), id);

        view.UpdateUI(Model);
    }
}

public interface ISubItemSlotView
{
    public void UpdateUI(SubItemSlotModel model);
}

public class SubItemSlot : MonoBehaviour, ISubItemSlotView
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Button slotBtn;

    private SubItemSlotPresenter presenter;

    public void Init(int id)
    {
        presenter ??= new SubItemSlotPresenter(this);
        presenter.Init(id);

        slotBtn.onClick.RemoveAllListeners();
        slotBtn.onClick.AddListener(() =>
        {
            var subItem = GameApplication.Instance.PlayerManager.GetSubItemById(id);
            var textInfo = presenter.Model.textInfo;

            textInfo.DescriptionKR = textInfo.DescriptionKR.StatDescription(textInfo.DescriptionKR, subItem);

            var tooltipBox = Instantiate(Resources.Load<TooltipBox>("Prefabs/UI/TooltipBox/TooltipBox"));
            tooltipBox.Init(presenter.Model.textInfo, Input.mousePosition);
        });
    }

    public void UpdateUI(SubItemSlotModel model)
    {
        iconImg.sprite = Resources.Load<Sprite>(model.iconInfo.Path);
    }
}
