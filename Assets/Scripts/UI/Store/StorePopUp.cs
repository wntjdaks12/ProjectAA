using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorePopUpModel
{
}
public class StorePopUpPresenter
{
    private StorePopUpModel model;
    private IStorePopUpView view;

    public StorePopUpPresenter(IStorePopUpView view)
    {
        this.view = view;
        model = new StorePopUpModel();
    }

    public void Init()
    {
        view.UpdateUI(model);
    }
}

public interface IStorePopUpView
{
    public void UpdateUI(StorePopUpModel model);
}

public class StorePopUp : PopUp, IStorePopUpView
{
    [SerializeField] private Button runeBtn;
    [SerializeField] private Button weaponBtn;
    [SerializeField] private Button subItemBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private TextMeshProUGUI runeText;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private TextMeshProUGUI subItemText;
    [SerializeField] private GameObject runeCont;
    [SerializeField] private GameObject weaponCont;
    [SerializeField] private GameObject subItemCont;

    private StorePopUpPresenter presenter;

    public void Init()
    {
        presenter ??= new StorePopUpPresenter(this);
        presenter.Init();

        weaponCont.SetActive(false);
        runeCont.SetActive(false);

        runeBtn.onClick.RemoveAllListeners();
        runeBtn.onClick.AddListener(() =>
        {
            if (runeCont.activeSelf)
            {
                runeCont.SetActive(false);

                runeText.text = "�� ����  ��";
            }
            else
            {
                runeCont.SetActive(true);

                runeText.text = "�� ����  ��";

            }
        });

        weaponBtn.onClick.RemoveAllListeners();
        weaponBtn.onClick.AddListener(() =>
        {
            if (weaponCont.activeSelf)
            {
                weaponCont.SetActive(false);

                weaponText.text = "����  ��";
            }
            else
            {
                weaponCont.SetActive(true);

                weaponText.text = "����  ��";

            }
        });

        subItemBtn.onClick.RemoveAllListeners();
        subItemBtn.onClick.AddListener(() =>
        {
            if (subItemCont.activeSelf)
            {
                subItemCont.SetActive(false);

                subItemText.text = "���� ������  ��";
            }
            else
            {
                subItemCont.SetActive(true);

                subItemText.text = "���� ������  ��";

            }
        });

        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() =>
        {
            OnHide();
        });

        OnShow();
    }

    public void UpdateUI(StorePopUpModel model)
    {
    }
}
