using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotPopUpModel
{
    public HeroContentsSlotPresenter HeroContentsSlotPresenter { get; set; }
    public int Id { get; set; }

    public enum QuickSlotTypes
    {
        Weapon, Rune
    }
    public QuickSlotTypes QuickSlotType { get; set; }
}

public class QuickSlotPopUpPresenter
{
    public QuickSlotPopUpModel Model { get; private set; }
    private IQuickSlotPopUpView view;

    public QuickSlotPopUpPresenter(IQuickSlotPopUpView view)
    {
        this.view = view;
        Model = new QuickSlotPopUpModel();
    }

    public void Init(HeroContentsSlotPresenter heroContentsSlotPresenter)
    {
        Model.HeroContentsSlotPresenter = heroContentsSlotPresenter;

        view.UpdateUI(Model);
    }
}

public interface IQuickSlotPopUpView
{
    public void UpdateUI(QuickSlotPopUpModel model);
}

public class QuickSlotPopUp : PopUp, IQuickSlotPopUpView
{
    [SerializeField] private QuickSlot quickSlot;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Transform parent;

    private QuickSlotPopUpPresenter presenter;

    public void Init(QuickSlotPopUpModel.QuickSlotTypes quickSlotType,HeroContentsSlotPresenter heroContentsSlotPresenter, Vector3 position)
    {
        presenter ??= new QuickSlotPopUpPresenter(this);
        presenter.Init(heroContentsSlotPresenter);

        presenter.Model.QuickSlotType = quickSlotType;

        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() =>
        {
            switch (quickSlotType)
            {
                case QuickSlotPopUpModel.QuickSlotTypes.Rune: GameApplication.Instance.PlayerManager.DivestRune(presenter.Model.HeroContentsSlotPresenter.Model.HeroInfo.id, presenter.Model.HeroContentsSlotPresenter.Model.HeroInfo.equipRuneId); break;
                case QuickSlotPopUpModel.QuickSlotTypes.Weapon: GameApplication.Instance.PlayerManager.DivestWeapon(presenter.Model.HeroContentsSlotPresenter.Model.HeroInfo.id, presenter.Model.HeroContentsSlotPresenter.Model.HeroInfo.equipWeaponId); break;
            }
            
            presenter.Model.HeroContentsSlotPresenter.Init();

            OnHide();
        });

        for (int i = 0; i < parent.childCount; i++)
        {
            if(i != 0)
                DestroyImmediate(parent.GetChild(parent.childCount - 1).gameObject);
        }

        switch (quickSlotType)
        {
            case QuickSlotPopUpModel.QuickSlotTypes.Rune:
                for (int i = 0; i < GameApplication.Instance.PlayerManager.GetDBRuneInfoCount(); i++)
                {
                    Instantiate(quickSlot, parent).Init(presenter, GameApplication.Instance.PlayerManager.GetDBRuneInfoByIndex(i).id);
                }

                break;
            case QuickSlotPopUpModel.QuickSlotTypes.Weapon:
                for (int i = 0; i < GameApplication.Instance.PlayerManager.GetDBWeaponInfoCount(); i++)
                {
                    Instantiate(quickSlot, parent).Init(presenter, GameApplication.Instance.PlayerManager.GetDBWeaponInfoByIndex(i).id);
                }

                break;
        }

        transform.position = position;

        OnShow();
    }

    public void UpdateUI(QuickSlotPopUpModel model)
    {
    }
}
