using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CustomTileCreator : Editor
{
    [MenuItem("Assets/Create/Custom Tiles From Selected Sprites")]
    public static void CreateCustomTiles()
    {

        Object[] sprites = Selection.GetFiltered(typeof(Sprite), SelectionMode.Unfiltered);

        if (sprites.Length == 0)
        {
            EditorUtility.DisplayDialog("No Sprites Selected", "Please select one or more sprites to create tiles.", "OK");
            return;
        }

        string path = EditorUtility.OpenFolderPanel("Select Folder to Save Tiles", "Assets", "");

        if (string.IsNullOrEmpty(path))
            return;

        // Convert the absolute path to a relative Unity path
        path = FileUtil.GetProjectRelativePath(path);

        foreach (Object spriteObject in sprites)
        {
            Sprite sprite = spriteObject as Sprite;
            if (sprite == null)
                continue;

            // Create a new instance of your CustomTile
            ExtendedTile tile = ScriptableObject.CreateInstance<ExtendedTile>();
            tile.sprite = sprite;
            tile.name = sprite.name;

            // Save the tile asset
            string assetPath = Path.Combine(path, sprite.name + ".asset");
            AssetDatabase.CreateAsset(tile, assetPath);
        }

        AssetDatabase.Refresh();
        
    }
}
