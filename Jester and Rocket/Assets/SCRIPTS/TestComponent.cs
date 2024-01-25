using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestComponent : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        var gameWindow = EditorWindow
            .GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
        gameWindow.Focus();
        gameWindow.SendEvent(new Event
        {
            button = 0,
            clickCount = 1,
            type = EventType.MouseDown,
            mousePosition = gameWindow.rootVisualElement.contentRect.center
        });
#endif

        Cursor.visible = false;
    }
}