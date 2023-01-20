using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binding
{
    private Action UnbindAction { get; set; }

    public Binding (Action unbindAction)
    {
        UnbindAction = unbindAction;
    }

    public void Unbind ()
    {
        UnbindAction.Invoke();
    }
}
