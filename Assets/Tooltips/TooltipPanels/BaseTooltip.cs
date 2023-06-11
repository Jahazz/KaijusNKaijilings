using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooltips.UI
{
    public abstract class BaseTooltip : MonoBehaviour
    {
        public abstract void Initialize (TooltipType type, string ID);

        public void Close ()
        {
            Destroy(gameObject);
        }
    }
}

