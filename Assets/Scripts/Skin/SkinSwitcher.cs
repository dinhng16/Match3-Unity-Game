using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "My Assets/Skin Switcher")]
public class SkinSwitcher: ScriptableObject
{
    [SerializeField] private List<SpriteRenderer> renderers;

    [SerializeField] private List<Sprite> skinNormal;
    [SerializeField] private List<Sprite> skinFish;
    
    [ContextMenu("SwitchSkinNormal")]
    public void SwitchSkinNormal()
    {
        for (int i = 0; i < renderers.Count; i += 1)
        {
            var renderer = renderers[i];
            renderer.sprite = skinNormal[i];
            EditorUtility.SetDirty(renderer);
        }
    }
    
    [ContextMenu("SwitchSkinFish")]
    public void SwitchSkinFish()
    {
        for (int i = 0; i < renderers.Count; i += 1)
        {
            var renderer = renderers[i];
            renderer.sprite = skinFish[i];
            EditorUtility.SetDirty(renderer);
        }
    }
}