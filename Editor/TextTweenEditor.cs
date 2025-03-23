using UnityEditor;

namespace TextTween.Editor {
    [CustomEditor(typeof(TweenManager))]
    public class TextTweenEditor : UnityEditor.Editor {
        private TweenManager tweenManager;
        private SerializedProperty _progressProperty;
        private SerializedProperty _offsetProperty;
        private SerializedProperty _textProperty;
        private SerializedProperty _modifiersProperty;

        private void OnEnable() {
            tweenManager = (TweenManager) target;
            _progressProperty = serializedObject.FindProperty("Progress");
            _offsetProperty = serializedObject.FindProperty("Offset");
            _textProperty = serializedObject.FindProperty("_text");
            _modifiersProperty = serializedObject.FindProperty("_modifiers");
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.Slider(_progressProperty, 0, 1, "Progress");
            EditorGUILayout.Slider(_offsetProperty, 0, 1, "Offset");
            EditorGUILayout.PropertyField(_textProperty);
            EditorGUILayout.PropertyField(_modifiersProperty);
            if (!EditorGUI.EndChangeCheck()) return;
            tweenManager.CreateCharDataArray();
            serializedObject.ApplyModifiedProperties();
        }
    }
}