using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUp;

    private static List<PopUp> popUps;

    protected virtual void Awake()
    {
        popUps ??= new List<PopUp>();
        popUps.Add(this);
    }

    public void OnShow()
    {
        popUp.SetActive(true);
    }

    public void OnHide()
    {
        popUp.SetActive(false);
    }

    public static T ReturnPopUp<T>() where T : PopUp
    {
        return popUps.Where(x => x is T).Select(x => (T)x).FirstOrDefault();
    }
}
