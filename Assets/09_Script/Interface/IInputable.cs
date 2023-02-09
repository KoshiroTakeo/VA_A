//======================================================================
// interface IInputable<T>.cs
//======================================================================
// �J������
//
// 2022/11/01 ���̓v���[���^�[
// 
//
//======================================================================
public interface IInputable
{
    // ���R���g���[���[�̑���
    void PressTriggerAction_L(bool trigger);
    void HoldTriggerAction_L(float value);
    void PressGripAction_L(bool trigger);
    void HoldGripAction_L(float value);
    void PressButton_X(bool trigger);
    void PressButton_Y(bool trigger);
    void PressButton_Menu(bool trigger);

    // �E�R���g���[���[�̑���
    void PressTriggerAction_R(bool trigger);
    void HoldTriggerAction_R(float value);
    void PressGripAction_R(bool trigger);
    void HoldGripAction_R(float value);
    void PressButton_A(bool trigger);
    void PressButton_B(bool trigger);
    void PressButton_Start(bool trigger);

}
