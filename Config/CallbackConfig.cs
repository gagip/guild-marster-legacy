using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CallbackConfig 
{
    // 생명체가 죽음
    public delegate void EntityDieCallback(Entity entity);

    // 모험 시작
    public delegate void AdventureStartCallback();

    // 모험 종료
    public delegate void AdventureEndCallback();

    // 라운드 종료
    public delegate void RoundEndCallback();
}
