using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    // 사전에 정의된 비 휘발성 데이터
    private GameDataContainer presetData;
    public GameDataContainer PresetData
    {
        get => presetData ??= new GameDataContainer();
    }

    // 런타임 중 할당되는 휘발성 데이터
    private GameDataContainer runTimeData;
    public GameDataContainer RunTimeData
    {
        get => runTimeData ??= new GameDataContainer();
    }

    private void Awake()
    {
        PresetData.LoadData<TextInfo>(nameof(TextInfo), "JsonDatas/TextInfo"); // 텍스트 정보
        PresetData.LoadData<PrefabInfo>(nameof(PrefabInfo), "JsonDatas/PrefabInfo"); // 프리팹 정보
        PresetData.LoadData<Stat>(nameof(Stat), "JsonDatas/Stat"); // 스탯
        PresetData.LoadData<Hero>(nameof(Hero), "JsonDatas/Hero"); // 히어로
        PresetData.LoadData<Weapon>(nameof(Weapon), "JsonDatas/Weapon"); // 무기
        PresetData.LoadData<Land>(nameof(Land), "JsonDatas/Land"); // 부지
        PresetData.LoadData<Monster>(nameof(Monster), "JsonDatas/Monster"); // 몬스터
        PresetData.LoadData<HeadBar>(nameof(HeadBar), "JsonDatas/HeadBar"); // 헤드바
        PresetData.LoadData<Skill>(nameof(Skill), "JsonDatas/Skill"); // 스킬
        PresetData.LoadData<VFX>(nameof(VFX), "JsonDatas/VFX"); // 이펙트
        PresetData.LoadData<IconInfo>(nameof(IconInfo), "JsonDatas/IconInfo"); // 아이콘 정보
        PresetData.LoadData<DamageFont>(nameof(DamageFont), "JsonDatas/DamageFont"); // 데미지 폰트
        PresetData.LoadData<DropDataInfo>(nameof(DropDataInfo), "JsonDatas/DropDataInfo"); // 드랍 데이터 정보
        PresetData.LoadData<DropItem>(nameof(DropItem), "JsonDatas/DropItem"); // 드랍 아이템
        PresetData.LoadData<NotifyPopUp>(nameof(NotifyPopUp), "JsonDatas/NotifyPopUp"); // 알리는 팝업
    }
}
