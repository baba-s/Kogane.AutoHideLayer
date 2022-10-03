using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [FilePath( "UserSettings/Kogane/AutoHideLayerSetting.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class AutoHideLayerSetting : ScriptableSingleton<AutoHideLayerSetting>
    {
        [SerializeField] private string[] m_layerNames = Array.Empty<string>();

        public IReadOnlyList<string> LayerNames => m_layerNames;

        public void Save()
        {
            Save( true );
        }

        public void Set( string[] layerNames )
        {
            m_layerNames = layerNames;
        }
    }
}