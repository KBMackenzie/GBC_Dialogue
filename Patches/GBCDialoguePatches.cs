using HarmonyLib;
using GBC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DiskCardGame;
#pragma warning disable Publicizer001

namespace GBC_Dialogue.Patches;

[HarmonyPatch]
internal static class GBCDialoguePatches
{
    internal static List<GBCCharacterBase> CharactersToLoad = new();

    #region Patches

    [HarmonyPatch(typeof(CameraController), nameof(CameraController.Start))]
    [HarmonyPostfix]
    private static void ChangeSprite()
    {
        if (CharactersToLoad.Count == 0 || SceneManager.GetActiveScene().name != "GBC_CardBattle") return;

        Transform portraitPanel = GameObject.Find("DialogueHandler")
                                    .transform
                                    .Find("TextBox")
                                    .Find("PortraitPanel");

        GameObject pseudoPrefab = portraitPanel
                                    .Find("DialoguePortrait_Grimora")
                                    .gameObject;

        TextBox box = portraitPanel.parent.gameObject.GetComponent<TextBox>();

        foreach(GBCCharacterBase character in CharactersToLoad)
        {
            CreateDialoguePortrait(in pseudoPrefab, in portraitPanel, character, in box);
        }

        GameObject scrybeSpeakers = GameObject.Find("ScrybeSpeakers");
        var inBattle = scrybeSpeakers.GetComponent<InBattleDialogueSpeakers>();

        foreach(GBCCharacterBase character in CharactersToLoad)
        {
            DialogueSpeaker speaker = scrybeSpeakers.AddComponent<DialogueSpeaker>();
            speaker.characterId = CharacterLibrary.Get(character.Id);
            inBattle.speakers.Add(speaker);
        }
    }

    #endregion

    internal static GameObject CreateDialoguePortrait(in GameObject pseudoPrefab, in Transform parent,
        GBCCharacterBase character, in TextBox box)
    {
        GameObject newPortrait = GameObject.Instantiate(pseudoPrefab, parent);
        newPortrait.name = $"DialoguePortrait_{character.Name}";
        DialoguePortrait portrait = newPortrait.GetComponent<DialoguePortrait>();
        portrait.SetSprite(character.DefaultSprite
                           ?? character.EmotionSprites[0].sprite);

        DialogueSpeaker.Character id = CharacterLibrary.Add(character);
        portrait.characterId = id;
        portrait.emotionSprites = character.EmotionSprites;
        box.dialoguePortraits.Add(portrait);

        newPortrait.SetActive(false);
        return newPortrait;
    }
}