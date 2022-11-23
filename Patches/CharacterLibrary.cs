﻿using GBC;
using InscryptionAPI.Guid;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace GBC_Dialogue.Patches
{
    internal static class CharacterLibrary
    {
        private static Dictionary<string, DialogueSpeaker.Character> Characters = new();

        private static string Guid = Plugin.PluginGuid;

        public static DialogueSpeaker.Character Add(GBCCharacterBase character)
        {
            string id = character.Id;
            if (Characters.ContainsKey(id)) return Characters[id];

            var newCharacter = GuidManager.GetEnumValue<DialogueSpeaker.Character>(Guid, id);
            Characters.Add(id, newCharacter);
            return newCharacter;
        }

        public static DialogueSpeaker.Character Get(GBCCharacterBase character)
        {
            return Get(character.Id);
        }

        public static DialogueSpeaker.Character Get(string id)
        {
            if(!Characters.ContainsKey(id))
            {
                Plugin.myLogger.LogError($"Character ID not found: {id}");
                return default;
            }

            return Characters[id];
        }
    }
}