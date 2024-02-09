using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductSlotModel
{ 
    public int Id { get; set; }
    public enum ProductTypes
    {
        Weapon, Rune, SubItem
    }
    public ProductTypes ProductType { get; set; }
}

public class ProductSlotPreesenter
{
    public ProductSlotModel Model { get; private set; }
    private IProductSlotView view;

    public ProductSlotPreesenter(IProductSlotView view)
    {
        this.view = view;
        Model = new ProductSlotModel();
    }

    public void Init(int id)
    {
        Model.Id = id;

        view.UpdateUI(Model);
    }
}

public interface IProductSlotView
{
    public void UpdateUI(ProductSlotModel model);
}

public class ProductSlot : MonoBehaviour, IProductSlotView
{
    [SerializeField] private Image iconImg;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button productBtn;

    [SerializeField] private int id;

    private ProductSlotPreesenter presenter;

    public ProductSlotModel.ProductTypes temp;// ¼öÁ¤

    private void Start()
    {
        Init(id);
        presenter.Model.ProductType = temp;
    }

    public void Init(int id)
    {
        presenter ??= new ProductSlotPreesenter(this);
        presenter.Init(id);

        buyBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.AddListener(() =>
        {
            PopUp.ReturnPopUp<BuyPopUp>().Init(presenter, id);
        });

        productBtn.onClick.RemoveAllListeners();
        productBtn.onClick.AddListener(() =>
        {
            var textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), id);

            switch (temp)
            //switch (presenter.Model.ProductType)
            {
                case ProductSlotModel.ProductTypes.Weapon: break;

                case ProductSlotModel.ProductTypes.Rune:
                    var skills = GameApplication.Instance.EntityController.Spawn<Skill>(id);

                    textInfo.DescriptionKR = textInfo.DescriptionKR.SkillDescription(textInfo.DescriptionKR, skills);

                    break;

                case ProductSlotModel.ProductTypes.SubItem:  break;

            }

            Instantiate(Resources.Load<TooltipBox>("Prefabs/UI/TooltipBox/TooltipBox")).Init(textInfo, Input.mousePosition);
        });
    }
        
    public void UpdateUI(ProductSlotModel model)
    {
        iconImg.sprite = Resources.Load<Sprite>(GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), model.Id).Path);
        nameText.text = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), model.Id).NameKR;
    }
}
