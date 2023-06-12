
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class BookPageRepositionProcessor : InputProcessor<Vector2>
{
#if UNITY_EDITOR
    static BookPageRepositionProcessor ()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize ()
    {
        InputSystem.RegisterProcessor<BookPageRepositionProcessor>();
    }

    public override Vector2 Process (Vector2 value, InputControl control)
    {
        if (ObjectToCanvasRaycaster.IsCursorOverrideEnabled)
        {
            value = ObjectToCanvasRaycaster.CustomPagePosition;
        }
        return value;
    }
}