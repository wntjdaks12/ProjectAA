using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContentsModel
{ 
}

public class HeroContentsPresenter
{
    private HeroContentsModel model;
    private HeroContentsView view;

    public HeroContentsPresenter(HeroContentsView view)
    {
        this.view = view;
        model = new HeroContentsModel();
    }

    public void Init()
    {
        view.UpdateUI(model);
    }
}

public interface HeroContentsView
{
    public void UpdateUI(HeroContentsModel model);
}

public class HeroContents : MonoBehaviour, HeroContentsView
{
    [SerializeField] private HeroContentsSlot slot;
    [SerializeField] private Transform parent;

    private HeroContentsPresenter presenter;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        presenter ??= new HeroContentsPresenter(this);
        presenter.Init();
    }

    public void UpdateUI(HeroContentsModel model)
    {
        var count = GameApplication.Instance.PlayerManager.DBHeroInfos.Count;

        for (int i = 0; i < count; i++)
        {
            var slotObj = Instantiate(slot, parent);
            slotObj.Init(i);
        }
    }
}
