using UnityEditor;

public class MayaModelFix : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        ModelImporter modelImporter = (ModelImporter)assetImporter;
        modelImporter.useFileUnits = false;
    }
}
