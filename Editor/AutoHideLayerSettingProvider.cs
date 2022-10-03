using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class AutoHideLayerSettingProvider : SettingsProvider
    {
        private const string PATH = "Kogane/Auto Hide Layer";

        private Editor m_editor;

        private AutoHideLayerSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = AutoHideLayerSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            var setting = AutoHideLayerSetting.instance;

            if ( GUILayout.Button( "Select Layers" ) )
            {
                var checkBoxWindowDataArray = InternalEditorUtility.layers
                        .Select( x => new CheckBoxWindowData( x, setting.LayerNames.Any( y => x == y ) ) )
                        .ToArray()
                    ;

                CheckBoxWindow.Open
                (
                    title: "Select Layers",
                    dataList: checkBoxWindowDataArray,
                    onOk: _ =>
                    {
                        var layerNames = checkBoxWindowDataArray
                                .Where( x => x.IsChecked )
                                .Select( x => x.Name )
                                .ToArray()
                            ;

                        setting.Set( layerNames );
                        setting.Save();
                    }
                );
            }

            m_editor.OnInspectorGUI();

            if ( !changeCheckScope.changed ) return;

            setting.Save();
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new AutoHideLayerSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}