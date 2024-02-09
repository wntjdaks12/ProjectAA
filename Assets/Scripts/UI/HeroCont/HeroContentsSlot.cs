using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class HeroContentsSlotModel
{ 
    public DBHeroInfo HeroInfo { get; set; }
    public int Index { get; set; }
}

public class HeroContentsSlotPresenter
{
    public HeroContentsSlotModel Model { get; private set; }
    private HeroContentSlotView view;

    public HeroContentsSlotPresenter(HeroContentSlotView view, int index)
    {
        this.view = view;
        Model = new HeroContentsSlotModel();
        Model.Index = index;
        Model.HeroInfo = GameApplication.Instance.PlayerManager.DBHeroInfos[index];
    }

    public void Init()
    {
        view.UpdateUI(Model);
    }
}

public interface HeroContentSlotView
{
    public void UpdateUI(HeroContentsSlotModel model);
}

public class HeroContentsSlot : MonoBehaviour, HeroContentSlotView
{
    [SerializeField] private Image HeroImg;
    [SerializeField] private Image activeSkillImg;
    [SerializeField] private Image runeImg;
    [SerializeField] private Image weaponImg;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private Button activeSkillBtn;
    [SerializeField] private Button RuneBtn;
    [SerializeField] private Button weaponBtn;
    [SerializeField] private SubItemContents subItemContents;

    private HeroContentsSlotPresenter presenter;

    public void Init(int index)
    {
        presenter ??= new HeroContentsSlotPresenter(this, index);
        presenter.Init();

        activeSkillBtn.onClick.RemoveAllListeners();
        activeSkillBtn.onClick.AddListener(() =>
        {
            var skills = GameApplication.Instance.PlayerManager.GetSkillTools(presenter.Model.HeroInfo.id);
            var textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), presenter.Model.HeroInfo.skillIds[0]);

            textInfo.DescriptionKR = textInfo.DescriptionKR.SkillDescription(textInfo.DescriptionKR, skills[0].Skill);

            var tooltipBox = Instantiate(Resources.Load<TooltipBox>("Prefabs/UI/TooltipBox/TooltipBox"));
            tooltipBox.Init(textInfo, Input.mousePosition);
        });

        RuneBtn.onClick.RemoveAllListeners();
        RuneBtn.onClick.AddListener(() =>
        {/*
            var dbRuneInfo = GameApplication.Instance.PlayerManager.GetDBRuneInfoById(presenter.Model.HeroInfo.equipRuneId);

            if (dbRuneInfo != null)
            {
                var skill = GameApplication.Instance.GameModel.PresetData.ReturnData<Skill>(nameof(Skill), dbRuneInfo.id);
                var textInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), dbRuneInfo.id);

                textInfo.DescriptionKR = textInfo.DescriptionKR.SkillDescription(textInfo.DescriptionKR, skill);

                var tooltipBox = Instantiate(Resources.Load<TooltipBox>("Prefabs/UI/TooltipBox/TooltipBox"));
                tooltipBox.Init(textInfo, Input.mousePosition);
            }*/

            if (GameApplication.Instance.PlayerManager.GetDBRuneInfoCount() > 0) PopUp.ReturnPopUp<QuickSlotPopUp>().Init(QuickSlotPopUpModel.QuickSlotTypes.Rune, presenter, Input.mousePosition);
        });

        weaponBtn.onClick.RemoveAllListeners();
        weaponBtn.onClick.AddListener(() =>
        {
            if (GameApplication.Instance.PlayerManager.GetDBWeaponInfoCount() > 0) PopUp.ReturnPopUp<QuickSlotPopUp>().Init(QuickSlotPopUpModel.QuickSlotTypes.Weapon, presenter, Input.mousePosition);
        });
    }

    public void UpdateUI(HeroContentsSlotModel model)   
    {
        NameText.text = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), model.HeroInfo.id).NameKR;

        HeroImg.sprite = Resources.Load<Sprite>(GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), model.HeroInfo.id).Path);
        activeSkillImg.sprite = Resources.Load<Sprite>(GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), model.HeroInfo.skillIds[0]).Path);
        if (model.HeroInfo.equipRuneId != 0)
        {
            runeImg.sprite = Resources.Load<Sprite>(GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), model.HeroInfo.equipRuneId).Path);

            runeImg.gameObject.SetActive(true);
        }
        else
        {
            runeImg.sprite = null;

            runeImg.gameObject.SetActive(false);
        }

        if (model.HeroInfo.equipWeaponId != 0)
        {
            weaponImg.sprite = Resources.Load<Sprite>(GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), model.HeroInfo.equipWeaponId).Path);
        }
        else
        {
            weaponImg.sprite = null;
        }

        subItemContents.Init(model);
    }
}
