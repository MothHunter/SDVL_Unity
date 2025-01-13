using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

[CustomEditor(typeof(ExtendedTile))]
public class CustomTileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
    
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Tile tile = (Tile)target;
        if (tile.sprite != null)
        {
            Texture2D tileIcon = new Texture2D(width, height);
            Texture2D spritePreview = AssetPreview.GetAssetPreview(tile.sprite);
            EditorUtility.CopySerialized(spritePreview, tileIcon);
            EditorUtility.SetDirty(tile);
            return tileIcon;
        }

        return base.RenderStaticPreview(assetPath, subAssets, width, height);
    }

}
