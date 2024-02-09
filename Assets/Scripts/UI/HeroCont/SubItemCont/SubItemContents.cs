using UnityEngine;

public class SubItemContentsModel
{
    public HeroContentsSlotModel HeroContentsSlotModel { get; set; }
}

public class SubItemContentsPresenter
{
    private SubItemContentsModel model;
    private ISubItemContentsView view;

    public SubItemContentsPresenter(ISubItemContentsView view)
    {
        this.view = view;
        model = new SubItemContentsModel();
    }

    public void Init(HeroContentsSlotModel heroContentsSlotModel)
    {
        model.HeroContentsSlotModel = heroContentsSlotModel;

        view.UpdateUI(model);
    }
}

public interface ISubItemContentsView
{
    public void UpdateUI(SubItemContentsModel model);
}

public class SubItemContents : MonoBehaviour, ISubItemContentsView
{
    [SerializeField] private SubItemSlot subItemSlot;

    private SubItemContentsPresenter presenter;

    public void Init(HeroContentsSlotModel heroContentsSlotModel)
    {
        presenter ??= new SubItemContentsPresenter(this);
        presenter.Init(heroContentsSlotModel);
    }

    public void UpdateUI(SubItemContentsModel model)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
        }

        var equipSubItemIds = model.HeroContentsSlotModel.HeroInfo.equipSubItemIds;

        for (int i = 0; i < 10; i++)
        {
            var slotObj = Instantiate(subItemSlot, transform);

            if (i < equipSubItemIds.Count) slotObj.Init(equipSubItemIds[i]);
        }
    }
}
