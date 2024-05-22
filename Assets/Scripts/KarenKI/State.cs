using System;
using System.Threading;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void Enter(KarenBehavior _context);
    public abstract void Do(KarenBehavior _context);
    public abstract void FixedDo(KarenBehavior _context);
    public abstract void CheckState(KarenBehavior _context);
    public abstract void Exit(KarenBehavior _context);
}