using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    // ������ ���ǵ� �� �ֹ߼� ������
    private GameDataContainer presetData;
    public GameDataContainer PresetData
    {
        get => presetData ??= new GameDataContainer();
    }

    // ��Ÿ�� �� �Ҵ�Ǵ� �ֹ߼� ������
    private GameDataContainer runTimeData;
    public GameDataContainer RunTimeData
    {
        get => runTimeData ??= new GameDataContainer();
    }

    private void Awake()
    {
        PresetData.LoadData<TextInfo>(nameof(TextInfo), "JsonDatas/TextInfo"); // �ؽ�Ʈ ����
        PresetData.LoadData<PrefabInfo>(nameof(PrefabInfo), "JsonDatas/PrefabInfo"); // ������ ����
        PresetData.LoadData<Stat>(nameof(Stat), "JsonDatas/Stat"); // ����
        PresetData.LoadData<Hero>(nameof(Hero), "JsonDatas/Hero"); // �����
        PresetData.LoadData<Weapon>(nameof(Weapon), "JsonDatas/Weapon"); // ����
        PresetData.LoadData<Land>(nameof(Land), "JsonDatas/Land"); // ����
        PresetData.LoadData<Monster>(nameof(Monster), "JsonDatas/Monster"); // ����
        PresetData.LoadData<HeadBar>(nameof(HeadBar), "JsonDatas/HeadBar"); // ����
        PresetData.LoadData<Skill>(nameof(Skill), "JsonDatas/Skill"); // ��ų
        PresetData.LoadData<VFX>(nameof(VFX), "JsonDatas/VFX"); // ����Ʈ
        PresetData.LoadData<IconInfo>(nameof(IconInfo), "JsonDatas/IconInfo"); // ������ ����
        PresetData.LoadData<DamageFont>(nameof(DamageFont), "JsonDatas/DamageFont"); // ������ ��Ʈ
        PresetData.LoadData<DropDataInfo>(nameof(DropDataInfo), "JsonDatas/DropDataInfo"); // ��� ������ ����
        PresetData.LoadData<DropItem>(nameof(DropItem), "JsonDatas/DropItem"); // ��� ������
        PresetData.LoadData<NotifyPopUp>(nameof(NotifyPopUp), "JsonDatas/NotifyPopUp"); // �˸��� �˾�
    }
}
