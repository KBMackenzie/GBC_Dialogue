using DiskCardGame;
using GBC;
using System.Collections.Generic;
using UnityEngine;
#nullable enable

namespace GBC_Dialogue.Patches;

public class GBCCharacterBase
{
    public readonly string Guid, Name;
    public Sprite? DefaultSprite;
    public List<DialoguePortrait.EmotionSprite> EmotionSprites = new();
    public string Id => $"{Guid}_{Name}";

    public GBCCharacterBase(string guid, string name, Sprite? sprite = null)
    {
        Guid = guid;
        Name = name;
        DefaultSprite = sprite;
    }

    // Create and register.
    public static GBCCharacterBase New(string guid, string name, Sprite? sprite = null)
    {
        GBCCharacterBase character = new GBCCharacterBase(guid, name, sprite);
        GBCDialoguePatches.CharactersToLoad.Add(character);
        return character;
    }

    public void AddEmotion(Emotion emotion, Sprite sprite)
    {
        EmotionSprites.Add(CreateEmotion(emotion, sprite));
    }

    public void AddEmotions(params DialoguePortrait.EmotionSprite[] emotions)
    {
        EmotionSprites.AddRange(emotions);
    }

    public static DialoguePortrait.EmotionSprite CreateEmotion(Emotion e, Sprite s)
    {
        return new DialoguePortrait.EmotionSprite() { emotion = e, sprite = s };
    }
}
