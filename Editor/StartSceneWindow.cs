using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace VK.Editor.StartSceneSetter
{
    /// <summary>
    /// Forces Unity to start a specific scene when entering Play Mode, 
    /// regardless of which scene is currently open.
    /// </summary>
    [InitializeOnLoad]
    public class StartSceneWindow : EditorWindow
    {
        private const string PREF_GUID = "StartScene_GUID";
        private const string PREF_ENABLED = "StartScene_Enabled";

        private SceneAsset _targetScene;
        private bool _isEnabled;

        // This constructor runs automatically when Unity loads/compiles
        static StartSceneWindow()
        {
            EditorApplication.delayCall += ApplySettingsOnLoad;
        }

        private static void ApplySettingsOnLoad()
        {
            bool enabled = EditorPrefs.GetBool(PREF_ENABLED, false);
            string guid = EditorPrefs.GetString(PREF_GUID, string.Empty);

            if (enabled && !string.IsNullOrEmpty(guid))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                EditorSceneManager.playModeStartScene = scene;
            }
        }

        [MenuItem("Tools/VK/Play Mode Scene Setter")]
        public static void ShowWindow() => GetWindow<StartSceneWindow>("Start Scene");

        private void OnEnable()
        {
            _isEnabled = EditorPrefs.GetBool(PREF_ENABLED, false);
            string guid = EditorPrefs.GetString(PREF_GUID, string.Empty);
            string path = AssetDatabase.GUIDToAssetPath(guid);
            _targetScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Force Unity to always play a specific scene.", MessageType.Info);
            EditorGUILayout.Space();

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                _isEnabled =
                    EditorGUILayout.ToggleLeft(" Enable Forced Start Scene", _isEnabled, EditorStyles.boldLabel);

                EditorGUI.BeginDisabledGroup(!_isEnabled);
                _targetScene =
                    (SceneAsset)EditorGUILayout.ObjectField("Target Scene", _targetScene, typeof(SceneAsset), false);
                EditorGUI.EndDisabledGroup();

                if (check.changed)
                {
                    Save();
                }
            }

            EditorGUILayout.Space();
            if (_isEnabled && _targetScene == null)
            {
                EditorGUILayout.HelpBox("No scene selected! System is currently inactive.", MessageType.Warning);
            }
        }

        private void Save()
        {
            EditorPrefs.SetBool(PREF_ENABLED, _isEnabled);

            if (_targetScene != null)
            {
                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_targetScene));
                EditorPrefs.SetString(PREF_GUID, guid);
                EditorSceneManager.playModeStartScene = _isEnabled ? _targetScene : null;
            }
            else
            {
                EditorPrefs.DeleteKey(PREF_GUID);
                EditorSceneManager.playModeStartScene = null;
            }

            Repaint();
        }
    }
}