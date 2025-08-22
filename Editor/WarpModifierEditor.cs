using UnityEditor;
using UnityEngine;
using TextTween.Modifiers;

namespace TextTween.Editor
{
    [CustomEditor(typeof(WarpModifier))]
    public class WarpModifierEditor : UnityEditor.Editor
    {
        private float _lastIntensity;
        private AnimationCurve _lastWarpCurve;
        private bool _hasInitialized;

        private void OnEnable()
        {
            var warpModifier = (WarpModifier)target;
            _lastIntensity = warpModifier.Intensity;
            _lastWarpCurve = warpModifier.WarpCurve != null ? new AnimationCurve(warpModifier.WarpCurve.keys) : null;
            _hasInitialized = true;
        }

        public override void OnInspectorGUI()
        {
            var warpModifier = (WarpModifier)target;
            
            EditorGUI.BeginChangeCheck();

            DrawDefaultInspector();
            
            if (EditorGUI.EndChangeCheck() && _hasInitialized)
            {
                bool intensityChanged = !Mathf.Approximately(_lastIntensity, warpModifier.Intensity);
                bool curveChanged = HasCurveChanged(_lastWarpCurve, warpModifier.WarpCurve);
                
                if (intensityChanged || curveChanged)
                {
                    RefreshTextTweenManager(warpModifier);
                    
                    _lastIntensity = warpModifier.Intensity;
                    _lastWarpCurve = warpModifier.WarpCurve != null ? new AnimationCurve(warpModifier.WarpCurve.keys) : null;
                }
            }
        }

        private void RefreshTextTweenManager(WarpModifier warpModifier)
        {
            var textTweenManager = warpModifier.GetComponentInParent<TextTweenManager>();
            
            if (textTweenManager == null)
            {
                var allManagers = FindObjectsOfType<TextTweenManager>();
                foreach (var manager in allManagers)
                {
                    if (manager.Modifiers != null && manager.Modifiers.Contains(warpModifier))
                    {
                        textTweenManager = manager;
                        break;
                    }
                }
            }

            if (textTweenManager != null)
            {
                textTweenManager.Apply();
                
                SceneView.RepaintAll();
            }
        }

        private bool HasCurveChanged(AnimationCurve curve1, AnimationCurve curve2)
        {
            if (curve1 == null && curve2 == null) return false;
            if (curve1 == null || curve2 == null) return true;
            
            if (curve1.keys.Length != curve2.keys.Length) return true;
            
            for (int i = 0; i < curve1.keys.Length; i++)
            {
                var key1 = curve1.keys[i];
                var key2 = curve2.keys[i];
                
                if (!Mathf.Approximately(key1.time, key2.time) ||
                    !Mathf.Approximately(key1.value, key2.value) ||
                    !Mathf.Approximately(key1.inTangent, key2.inTangent) ||
                    !Mathf.Approximately(key1.outTangent, key2.outTangent))
                {
                    return true;
                }
            }

            return false;
        }
    }
}