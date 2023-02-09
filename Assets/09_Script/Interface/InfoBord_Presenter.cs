using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInfoBord
{
    public VR.Players.PlayerParameter ThrowData { get; set; }
}



public class InfoBord_Presenter : MonoBehaviour
{
    // View �� model
    [SerializeField] private InfoBord view;
    [SerializeField] private VR.Players.MasterPlayer model; //�ˑ�


    // �ˑ��t�]�̂���
    private IInfoBord Iinfo;

    public bool Set()
    {
        // model�̕ω���View�ɓ`����
        Iinfo = GetComponent<IInfoBord>();

        model = GameObject.FindWithTag("Player").GetComponent<VR.Players.MasterPlayer>();

        Iinfo.ThrowData = model.ThrowData();

        return true;
    }

    private void Start()
    {
        
    }
}


