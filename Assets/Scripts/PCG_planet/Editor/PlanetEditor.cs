using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;
    Editor shapeEditor;
    Editor colorEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.RandomGeneratePlanet("Random");
            }
        }

        if(GUILayout.Button("Generate Planet"))
        {
            planet.RandomGeneratePlanet("Random");
        }

        if (GUILayout.Button("Random Generate Planet"))
        {
            planet.RandomGeneratePlanet("Random");
        }

        DrawSettingEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingFoldout, ref shapeEditor);
        DrawSettingEditor(planet.colorSettings, planet.OnColorSettingsUpdated, ref planet.colorSettingFoldout, ref colorEditor);
    }

    void DrawSettingEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null) 
        { 
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout) 
                { 
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if(check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        planet = (Planet)target;
    }

}
