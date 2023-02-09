using UnityEngine;
using UnityEngine.AI;
using VR.Enemys;

public class Attack : EnemyState                                                                
{
    public Attack(GameObject _npc, Transform _player, EnemyData _enemyData)
        : base(_npc, _player, _enemyData)                                              
    {
        name = STATE.ATTACK;                                                                
    }

    //virtual�C���q���t����Enter��p����
    public override void Enter()                                                          
    {
        base.Enter();                                                                     
    }

    //virtual�C���q���t����Updata��p����
    public override void Updata()                                                        
    {
        if (enemyData.nLife <= 0) NextState(new Knockout(npc, player, enemyData));

        if (!CanAttackPlayer()) NextState(new Pursue(npc, player, enemyData));
    }

    //virtual�C���q���t����Exit��p����
    public override void Exit()                                                            
    {
        base.Exit();
    }
}
