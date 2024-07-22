using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EventManager : Singleton<EventManager>
{
    /*卡牌事件*/
    public Action<Card, int> onDraw; //抽取事件，表示了第几个卡位抽取了哪张卡
    public Action<Card, int> onUse; //使用事件，表示了第几个卡位使用了哪张卡
    public Action<Card, int> onDiscard; //弃置事件，表示了第几个卡位弃置了哪张卡
    public Action onShuffle;  //洗牌事件，没啥好说

    /*战斗事件*/
    public Action stuckShooting; //阻塞射击，当外界需要禁用玩家射击时触发此事件
    public Action applyShooting; //允许射击，阻塞射击过后用该事件取消阻塞
    public Action<BulletInfo> onBulletGenerate; //当创建子弹时，所有和子弹相关的物体共同修改它的info

    /*数据事件*/
    public Action<int> onLevelUp; //当升级时,传入新等级
}
