using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    [SerializeField]
    private DialogManager dialogManager;

    private IEnumerator Start()
    {
        dialogManager = DialogManager.Instance;
        yield return new WaitUntil(() => dialogManager.UpdateDialog());
    }
}
