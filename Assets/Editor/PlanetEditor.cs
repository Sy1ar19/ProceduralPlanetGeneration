using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet _planet;
    Editor _shapeEditor;
    Editor _colorEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                _planet.GeneratePlanet();
            }
        }

        if (GUILayout.Button("Generate Planet"))
        {
            _planet.GeneratePlanet();
        }

        DrawSettingEditor(_planet.ShapeSettings, _planet.OnShapeSettingsUpdated,
            ref _planet.ShapeSettingsFoldout, ref _shapeEditor);
        DrawSettingEditor(_planet.ColorSettings, _planet.OnColorSettingsUpdated,
            ref _planet.ColorSettingsFoldout, ref _colorEditor);
    }

    public void DrawSettingEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if(settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                            onSettingsUpdated();
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        _planet = (Planet)target;
    }
}
