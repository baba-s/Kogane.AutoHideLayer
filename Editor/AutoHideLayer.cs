using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class AutoHideLayer
    {
        private static readonly EditorFirstBootChecker CHECKER = new( "Kogane.AutoHideLayer" );

        static AutoHideLayer()
        {
            if ( !CHECKER.IsFirstBoot() ) return;

            foreach ( var layerName in AutoHideLayerSetting.instance.LayerNames )
            {
                var layer = LayerMask.NameToLayer( layerName );

                Tools.visibleLayers &= ~( 1 << layer );
                Tools.lockedLayers  |= 1 << layer;
            }
        }
    }
}