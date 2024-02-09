using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotModeel
{
    public QuickSlotPopUpPresenter QuickSlotPopUpPresenter { get; set; }
    public int Id { get; set; }
}

public class QuickSlotPresenter
{
    private QuickSlotModeel model;
    private IQuickSlotView view;

    public QuickSlotPresenter(IQuickSlotView view)
    {
        this.view = view;
        model = new QuickSlotModeel();
    }

    public void Init(QuickSlotPopUpPresenter quickSlotPopUpPresenter, int id)
    {
        model.QuickSlotPopUpPresenter = quickSlotPopUpPresenter;
        model.Id = id;

        view.UpdateUI(model);
    }
}

public interface IQuickSlotView
{
    public void UpdateUI(QuickSlotModeel model);
}

public class QuickSlot : MonoBehaviour, IQuickSlotView
{
    [SerializeField] private Image iconImg;
    [SerializeField] private Button slotBtn;

    private QuickSlotPresenter presenter;

    public void Init(QuickSlotPopUpPresenter quickSlotPopUpPresenter, int id)
    {
        presenter ??= new QuickSlotPresenter(this);
        presenter.Init(quickSlotPopUpPresenter, id);

        slotBtn.onClick.RemoveAllListeners();
        slotBtn.onClick.AddListener(() =>
        {
            switch (quickSlotPopUpPresenter.Model.QuickSlotType)
            {
                case QuickSlotPopUpModel.QuickSlotTypes.Weapon: GameApplication.Instance.PlayerManager.EquipWeapon(quickSlotPopUpPresenter.Model.HeroContentsSlotPresenter.Model.HeroInfo.id, id); break;
                case QuickSlotPopUpModel.QuickSlotTypes.Rune: GameApplication.Instance.PlayerManager.EquipRune(quickSlotPopUpPresenter.Model.HeroContentsSlotPresenter.Model.HeroInfo.id, id); break;
            }

            PopUp.ReturnPopUp<QuickSlotPopUp>().OnHide();

            quickSlotPopUpPresenter.Model.HeroContentsSlotPresenter.Init();
        });
    }

    public void UpdateUI(QuickSlotModeel model)
    {
        iconImg.sprite = Resources.Load<Sprite>(GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), model.Id).Path);
    }
}
