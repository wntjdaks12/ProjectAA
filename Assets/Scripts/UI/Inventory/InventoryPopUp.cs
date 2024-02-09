using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopUpModel
{ 
}

public class InventoryPopUpPresenter
{
    private InventoryPopUpModel model;
    private IInventoryPopUpView view;

    public InventoryPopUpPresenter(IInventoryPopUpView view)
    {
        this.view = view;
        model = new InventoryPopUpModel();
    }

    public void Init()
    {
        view.UpdateUI(model);
    }
}

public interface IInventoryPopUpView
{
    public void UpdateUI(InventoryPopUpModel model);
}


public class InventoryPopUp : PopUp, IInventoryPopUpView
{
    [SerializeField] private Transform parent;
    [SerializeField] private InventorySlot slot;
    [SerializeField] private Button exitBtn;

    private InventoryPopUpPresenter presenter;

    public void Init()
    {
        presenter ??= new InventoryPopUpPresenter(this);
        presenter.Init();

        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() =>
        {
            OnHide();
        });

        OnShow();
    }

    public void UpdateUI(InventoryPopUpModel model)
    {
        var itemInfos = GameApplication.Instance.PlayerManager.DBItemInfos;

        for (int i = 0; i < parent.childCount; i++)
        {
            DestroyImmediate(parent.GetChild(parent.childCount - 1).gameObject);
        }

        foreach (var itemInfo in itemInfos)
        {
            var slotObj = Instantiate(slot, parent);
            slotObj.Init(itemInfo.id);
        }
    }
}
