using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CallbackConfig 
{
    // ����ü�� ����
    public delegate void EntityDieCallback(Entity entity);

    // ���� ����
    public delegate void AdventureStartCallback();

    // ���� ����
    public delegate void AdventureEndCallback();

    // ���� ����
    public delegate void RoundEndCallback();
}
