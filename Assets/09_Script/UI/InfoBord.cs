//============================================================
// InfoBord.cs
//======================================================================
// �J������
//
// 2023/2/4 MVP�����@View 
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VR.Players;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class InfoBord : MonoBehaviour, IInfoBord
{

    public PlayerParameter Parameter;

    // �\������UI�B
    [SerializeField] Slider HPSlider;
    [SerializeField] Slider ShirdSlider;

    [SerializeField] TextMeshProUGUI DebugText;

    [SerializeField] InfoBord_Presenter presenter;

    bool Comp = false;

    // View�e�X�g
    public PlayerParameter ThrowData
    {
        get => Parameter;
        
        set => Parameter = value;
    }

    private void FixedUpdate()
    {
        if(Comp == false)
        {
            Comp = presenter.Set(); // �������񂩁H
        }
        
        Parameter = ThrowData;

        HPSlider.value = Parameter.CurrentHPValue();
        ShirdSlider.value = Parameter.CurrentShild();
    }
}
