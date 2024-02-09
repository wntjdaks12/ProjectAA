using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyPopUpModel
{
    public ProductSlotPreesenter ProductSlotPreesenter { get; set; }
    public int Id { get; set; }
}

public class BuyPopUpPresenter
{
    private BuyPopUpModel model;
    private IBuyPopUpPresenterView view;

    public BuyPopUpPresenter(IBuyPopUpPresenterView view)
    {
        this.view = view;
        model = new BuyPopUpModel();
    }

    public void Init(ProductSlotPreesenter productSlotPreesenter, int id)
    {
        model.Id = id;
        model.ProductSlotPreesenter = productSlotPreesenter;

        view.UpdateUI(model);
    }
}

public interface IBuyPopUpPresenterView
{
    public void UpdateUI(BuyPopUpModel model);
}

public class BuyPopUp : PopUp, IBuyPopUpPresenterView
{
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button cancelBtn;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private BuyPopUpPresenter presenter;

    public void Init(ProductSlotPreesenter productSlotPreesenter, int id)
    {
        presenter ??= new BuyPopUpPresenter(this);
        presenter.Init(productSlotPreesenter, id);

        buyBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.AddListener(() =>
        {
            if (productSlotPreesenter.Model.ProductType == ProductSlotModel.ProductTypes.Rune)
                GameApplication.Instance.PlayerManager.SetDBRuneInfo(new DBRuneInfo { id = id, count = 1 });
            else if (productSlotPreesenter.Model.ProductType == ProductSlotModel.ProductTypes.Weapon)
                GameApplication.Instance.PlayerManager.SetDBWeaponInfo(new DBWeaponInfo { id = id, count = 1 });
            else if (productSlotPreesenter.Model.ProductType == ProductSlotModel.ProductTypes.SubItem)
                GameApplication.Instance.PlayerManager.SetDBSubItemInfo(new DBSubItemInfo { id = id, count = 1 });

            OnHide();
        });

        cancelBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.AddListener(OnHide);

        OnShow();
    }

    public void UpdateUI(BuyPopUpModel model)
    {

        descriptionText.text = "<color=#CB4E77>" + GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), model.Id).NameKR + "</color><br>구매 하시겠습니까?";
    }
}
