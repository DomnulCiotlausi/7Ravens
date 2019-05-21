using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CharManager: MonoBehaviour
{

    public TextMeshPro talk;
    public PlayerCtrl playerCtrl;
    public Dialog dialog;
    public bool isChating = false;
    public bool isChated= false;

    abstract public void BehaviourAfterChating();
}
