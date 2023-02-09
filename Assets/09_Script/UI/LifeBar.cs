using UnityEngine;
using UnityEngine.UI;
using VR.Enemys;

public class LifeBar : MonoBehaviour
{
    [Header("Prefab����HPBar")]
    public GameObject bar;
    public static LifeBar instance;

    private GameObject PlayerObj;
    private GameObject EnemyObj;

    public Slider MainSlider;  // ���镔��
    public Slider LateSlider; // �x��č��镔��

    private int fMaxHP;
    private int fHp;

   
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        
        PlayerObj = GameObject.FindWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        // Player�����֏�Ɍ���
        this.transform.LookAt(PlayerObj.transform);

    }


    // ���݂�HP������
    public void SetLifeBar(GameObject _enemy, int _HP)
    {
        this.gameObject.transform.position = _enemy.transform.position;
        EnemyObj = _enemy;

        fHp = fMaxHP = _HP;
        MainSlider.value = 1;
        LateSlider.value = 1;

        // �e�ֈړ�
        transform.parent = EnemyObj.transform;
    }

    // HP�ɕω�����������
    public void UpdataLife(int _currentHP)
    {
        float _percent;
        fHp = _currentHP;
        _percent = (float)_currentHP / (float)fMaxHP;
        MainSlider.value = _percent;
        Debug.Log(_currentHP + "///" + _percent);
    }

    public void DirectionView(bool _onmovie)
    {
        if (_onmovie == false) return;


    }


}