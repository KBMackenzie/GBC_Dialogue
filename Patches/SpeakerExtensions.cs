using DiskCardGame;
using GBC;
#pragma warning disable Publicizer001

namespace GBC_Dialogue.Patches
{
    public static class SpeakerExtensions
    {
        public static DialogueSpeaker GetSpeaker(this InBattleDialogueSpeakers instance, 
            string guid, string name)
        {
            DialogueSpeaker.Character speaker = CharacterLibrary.Get($"{guid}_{name}");
            return instance.speakers.Find(x => x.characterId == speaker);
        }

        public static DialogueSpeaker GetSpeaker(this InBattleDialogueSpeakers instance,
            GBCCharacterBase character)
        {
            DialogueSpeaker.Character speaker = CharacterLibrary.Get(character);
            return instance.speakers.Find(x => x.characterId == speaker);
        }
    }
}
