using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDescContents : MonoBehaviour
{
    private static ObjectDescContents instance;
    public static ObjectDescContents Instance { get => instance == null ? instance = FindObjectOfType<ObjectDescContents>() : instance; }

    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI descriptionText;

    public void Init(int id)
    {
        descriptionText.text = GameApplication.Instance.GameModel.PresetData.ReturnData<TextInfo>(nameof(TextInfo), id).NameKR;

        animator.SetTrigger("OnShow");
    }
}
