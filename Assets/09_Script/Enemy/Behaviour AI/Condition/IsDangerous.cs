//===============================================
// HP���������ቺ���Ă��邩
//===============================================

using VR.Enemys;

public class IsDangerous : Node
{
    EnemyParameter parameter;

    float fhpvalue;
    float fborder;

    public IsDangerous(EnemyParameter _parameter, float _border)
    {
        parameter = _parameter;
        fborder = _border;
    }

    public override NodeState Evaluate()
    {
        return parameter.CurrentHPValue() <= fborder ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
