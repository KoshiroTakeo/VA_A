//============================================================
// �U���ȂǊǗ�
//======================================================================
// �J������
// 20220729:�p������̂��ߍč\�z
// 20221010:���킪�ςȈʒu�ɐ��������
//======================================================================
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR.Players
{
    public class ActionManager:MonoBehaviour
    {
        public enum Weapon
        {
            PRIMALY,
            SECONDALY,
            FREE,

            MAX
        }
        public Weapon HandState_L,HandState_R;

        GameObject PrimalyWeapon_L, PrimalyWeapon_R;
        GameObject SecondlyWeapon_L, SecondlyWeapon_R;


        // ����
        public ActionManager(PlayerData _data, Transform _handL, XRBaseController _XRContrpllerL, Transform _handR, XRBaseController _XRContrpllerR)
        {
            // ���r�ɑ���
            SetWeapon(_data, _handL, _XRContrpllerL, _handR, _XRContrpllerR);
        }

        
        // Weapon�̐���
        public void SetWeapon(PlayerData _data, Transform _handL, XRBaseController _XRContrpllerL, Transform _handR, XRBaseController _XRContrpllerR)
        {
            // �����������̂֕ύX
            PrimalyWeapon_L = _data.Left_PrimaryWeapon.GetComponent<WeaponBase>().InstantWeapon(_handL, _XRContrpllerL);
            PrimalyWeapon_L.SetActive(true);
            PrimalyWeapon_R = _data.Right_PrimaryWeapon.GetComponent<WeaponBase>().InstantWeapon(_handR, _XRContrpllerR);
            PrimalyWeapon_R.SetActive(true);

            // �����������̂֕ύX
            SecondlyWeapon_L = _data.Left_SecondaryWeapon.GetComponent<WeaponBase>().InstantWeapon(_handL, _XRContrpllerL);
            SecondlyWeapon_L.SetActive(false);
            SecondlyWeapon_R = _data.Right_SecondaryWeapon.GetComponent<WeaponBase>().InstantWeapon(_handR, _XRContrpllerR);
            SecondlyWeapon_R.SetActive(false);



        }

        // ����̐؂�ւ�
        public void ChangeWeapon_L()
        {
            HandState_L++;
            if((int)HandState_L == 2) HandState_L = 0;

            switch ((int)HandState_L)
            {
                case 0:
                    PrimalyWeapon_L.SetActive(true);
                    SecondlyWeapon_L.SetActive(false);
                    break;
                case 1:
                    PrimalyWeapon_L.SetActive(false);
                    SecondlyWeapon_L.SetActive(true);
                    break;
                case 2:
                    Debug.Log("�t���[�n���h");
                    break;
                default:
                    Debug.Log("��O");
                    break;
            }
        }

        public void ChangeWeapon_R()
        {
            HandState_R++;
            if ((int)HandState_R == 2) HandState_R = 0;

            switch ((int)HandState_R)
            {
                case 0:
                    PrimalyWeapon_R.SetActive(true);
                    SecondlyWeapon_R.SetActive(false);
                    break;
                case 1:
                    PrimalyWeapon_R.SetActive(false);
                    SecondlyWeapon_R.SetActive(true);
                    break;
                case 2:
                    Debug.Log("�t���[�n���h");
                    break;
                default:
                    Debug.Log("��O");
                    break;
            }
        }

        // ���X�L������
        public string PrimalySkill_L()
        {
            string debugtext = "";
            switch ((int)HandState_L)
            {
                case 0:
                    debugtext = PrimalyWeapon_L.GetComponent<IWeaponAction>().PrimalySkill();
                    break;
                case 1:
                    debugtext = SecondlyWeapon_L.GetComponent<IWeaponAction>().PrimalySkill();
                    break;
                case 2:
                    debugtext = "�t���[";
                    break;
                default:
                    debugtext = "��O" + HandState_L;
                    break;
            }

            return debugtext;

        }
        public void PrimalySkill_R()
        {
            switch ((int)HandState_R)
            {
                case 0:
                    PrimalyWeapon_R.GetComponent<IWeaponAction>().PrimalySkill();
                    break;
                case 1:
                    SecondlyWeapon_R.GetComponent<IWeaponAction>().PrimalySkill();
                    break;
                case 2:
                    Debug.Log("�t���[�n���h");
                    break;
                default:
                    Debug.Log("��O");
                    break;
            }
        }

        // ���X�L������
        public void SecondalySkill_L()
        {
            switch ((int)HandState_L)
            {
                case 0:
                    PrimalyWeapon_L.GetComponent<IWeaponAction>().SecondalySkill();
                    break;
                case 1:
                    SecondlyWeapon_L.GetComponent<IWeaponAction>().SecondalySkill();
                    break;
                case 2:
                    Debug.Log("�t���[�n���h");
                    break;
                default:
                    Debug.Log("��O");
                    break;
            }
        }
        public void SecondalySkill_R()
        {
            switch ((int)HandState_R)
            {
                case 0:
                    PrimalyWeapon_R.GetComponent<IWeaponAction>().SecondalySkill();
                    break;
                case 1:
                    SecondlyWeapon_R.GetComponent<IWeaponAction>().SecondalySkill();
                    break;
                case 2:
                    Debug.Log("�t���[�n���h");
                    break;
                default:
                    Debug.Log("��O");
                    break;
            }
        }
    }
}

