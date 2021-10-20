using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

//Note: This class uses UnityEditorInternal which is an undocumented internal feature
public class TagsLayersScenesBuilder : EditorWindow
{
    private const string targetFolder = "Scripts/Labels/";
    private const string tags = "Tags";
    private const string layers = "Layers";
    private const string scenes = "Scenes";
    private const string script = ".cs";


    [MenuItem("Labels/Generate labels")]
    static void RebuildTagsAndLayersClasses()
    {
        string folderPath = Application.dataPath + "/" + targetFolder;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(folderPath + tags + script, GetClassContent(tags, UnityEditorInternal.InternalEditorUtility.tags));
        File.WriteAllText(folderPath + layers + script, GetLayerClassContent(layers, UnityEditorInternal.InternalEditorUtility.layers));
        File.WriteAllText(folderPath + scenes + script, GetClassContent(scenes, EditorBuildSettingsScenesToNameStrings(EditorBuildSettings.scenes)));
        AssetDatabase.ImportAsset("Assets/" + targetFolder + tags + script, ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/" + targetFolder + layers + script, ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/" + targetFolder + scenes + script, ImportAssetOptions.ForceUpdate);
        Debug.Log("Rebuild Complete");
    }

    private static string[] EditorBuildSettingsScenesToNameStrings(EditorBuildSettingsScene[] scenes)
    {
        string[] sceneNames = new string[scenes.Length];
        for (int n = 0; n < sceneNames.Length; n++)
        {
            sceneNames[n] = Path.GetFileNameWithoutExtension(scenes[n].path);
        }

        return sceneNames;
    }

    private static string GetClassContent(string className, string[] labelsArray)
    {
        string output = "";
        output += "//This class is auto-generated do not modify (TagsLayersScenesBuilder.cs) - blog.almostlogical.com\n";
        output += "public static class " + className + "\n";
        output += "{\n";
        foreach (string label in labelsArray)
        {
            output += "\t" + BuildConstVariable(label) + "\n";
        }

        output += "}";
        return output;
    }

    private static string GetLayerClassContent(string className, string[] labelsArray)
    {
        string output = "";
        output += "//This class is auto-generated do not modify (TagsLayersScenesBuilder.cs) - blog.almostlogical.com\n";
        output += "public static class " + className + "\n";
        output += "{";


        //foreach (string label in labelsArray)
        //{
        //    output += "\t" + BuildConstVariable(label) + "\n";
        //}
        //output += "\n";

        //foreach (string label in labelsArray)
        //{
        //    output += "\t" + "public const int " + ToVarName(label) + "Int" + " = 1 << " + LayerMask.NameToLayer(label) + ";\n";
        //}
        foreach (string label in labelsArray)
        {
            output +=
                $"\n\tpublic static class {ToVarName(label)}" + "{" +
                $"\n\t\tpublic const string name = \"{label}\";" +
                $"\n\t\tpublic const int Int = {LayerMask.NameToLayer(label)};" +
                $"\n\t\tpublic const int IntShifted = 1 << Int;" +
                "\n\t}";
            output += "\n";
        }

        output += "}";
        return output;
    }

    private static string BuildConstVariable(string varName)
    {
        return "public const string " + ToVarName(varName) + " = " + '"' + varName + '"' + ";";
    }

    private static string ToVarName(string input)
    {
        return $"{input}".Replace(" ", "").Replace("_", "").Replace("/", "_");
    }
}