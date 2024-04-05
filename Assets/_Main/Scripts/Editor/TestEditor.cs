using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using ZTH.Unity.Tool;

public class TestEditor : OdinEditorWindow
{
    [MenuItem("Tools/Main/Controller Editor")]
    private static void OpenWindow()
    {
        GetWindow<TestEditor>().position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 600);
    }

    protected override IEnumerable<object> GetTargets()
    {
        yield return this;
    }

    protected override void DrawEditor(int index)
    {
        curtDrawingTargetIndex = index;
        moveDrawingTargetIndex = 0;

        //if (Check(() => SetString())) return;
    }

    private bool Check(Action action)
    {
        if (curtDrawingTargetIndex != moveDrawingTargetIndex++) return false;
        action();
        return true;
    }

    private void SetDefault()
    {
        base.DrawEditor(curtDrawingTargetIndex);
    }

    //private void SetString(string title)
    //{
    //    var value = CurrentDrawingTargets[curtDrawingTargetIndex] as string;
    //    value = SirenixEditorFields.TextField(title, value);

    //}

    private void SetTitle(string text)
    {
        var title = CurrentDrawingTargets[curtDrawingTargetIndex] as string;
        if (text != title) $"{text} not found".ThrowException();
        SirenixEditorGUI.Title(title, "", TextAlignment.Left, true);
    }

    private void SetImageColor(string label)
    {
        var image = CurrentDrawingTargets[curtDrawingTargetIndex] as Image;
        var color = SirenixEditorFields.ColorField(label, image.color);
        if (color == image.color) return;
        image.color = color;
        EditorUtility.SetDirty(image);
    }

    private void SetTextColor(string label)
    {
        var text = CurrentDrawingTargets[curtDrawingTargetIndex] as TMP_Text;
        var color = SirenixEditorFields.ColorField(label, text.color);
        if (color == text.color) return;
        text.color = color;
        EditorUtility.SetDirty(text);
    }

    private int curtDrawingTargetIndex;
    private int moveDrawingTargetIndex;
}
