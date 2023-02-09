//============================================================
// あの移動処理
//======================================================================
// 開発履歴
// 20220729:可用性向上のため再構築
//======================================================================
using UnityEngine;

namespace VR.Players
{
    public class HoverMover 
    {
        // 必要なもの
        public float fAccelCircleSize = 0.02f;
        public float fstoptime = 0.5f;
        public float fbreakvalue = 1f;
        public float fupvalue = 1f;
        public float fGravity = -0.8f;

        bool bFly = false;
        int nFlyTime = 0;
        
        public void HeadInclinationMove(CharacterController _character ,Vector3 _anchor, Vector3 _initirizepos, float _speed)
        {
            Vector3 vector = _anchor - _initirizepos;
            Vector3 direction = new Vector3();
            bool bStop = true;
            float faccel;

            // 限界速度補正値
            float fAnchorX = vector.x;
            float fAnchorZ = vector.z;
            float fAnchorY = vector.y * fGravity;

            // ブレーキ
            faccel = _speed * fbreakvalue;


            // 停止範囲外に出たとき走り出す
            if ((fAnchorZ > fAccelCircleSize || -fAccelCircleSize > fAnchorZ))
            {
                direction.z = fAnchorZ / fAccelCircleSize;
                bStop = true;
            }

            if ((fAnchorX > fAccelCircleSize || -fAccelCircleSize > fAnchorX))
            {
                direction.x = fAnchorX / fAccelCircleSize;
                bStop = true;
            }


            // 飛ぶ
            if (bFly == true)
            {
                //最初一回目は勢いよく飛びたい
                if (nFlyTime >= 10)
                {
                    nFlyTime--;
                }
                

                direction.y = _speed * fupvalue * (nFlyTime/2);
            }
            else
            {
                direction.y = fAnchorY;
                nFlyTime++;
                if(nFlyTime >= 120)
                {
                    nFlyTime = 120;
                }
            }

            


            // 移動処理
            if (!bStop)
            {
                _character.Move(direction * Time.fixedDeltaTime * faccel);
            }
            else
            {
                _character.Move(direction * Time.fixedDeltaTime * (faccel / fstoptime));
            }

            
        }

        public void BreakSpeed(bool _trigger)
        {
            if (_trigger == true) fbreakvalue = fbreakvalue * 0.9f;
            else fbreakvalue = 1;
        }

        public void Fly(bool _trigger)
        {
            if (_trigger == true) bFly = true;
            else bFly = false;
        }

        
    }
}
