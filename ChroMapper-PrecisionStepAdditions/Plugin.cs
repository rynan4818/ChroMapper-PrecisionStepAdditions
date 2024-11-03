using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace ChroMapper_PrecisionStepAdditions
{
    [Plugin("ChroMapper-PrecisionStepAdditions")]
    public class Plugin
    {
        public static Harmony _harmony;
        public const string HARMONY_ID = "com.github.username.ChroMapper-PrecisionStepAdditions"; //username を各自変更してください
        [Init]
        private void Init()
        {
            _harmony = new Harmony(HARMONY_ID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("ChroMapper-PrecisionStepAdditions Plugin has loaded!");
        }
        [Exit]
        private void Exit()
        {
            _harmony.UnpatchSelf();
            Debug.Log("ChroMapper-PrecisionStepAdditions Plugin has closed!");
        }
    }
}
