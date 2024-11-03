using HarmonyLib;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChroMapper_PrecisionStepAdditions
{
    [Plugin("Precision Step Additions")]
    public class Plugin
    {
        public static Harmony _harmony;
        public const string HARMONY_ID = "com.github.rynan4818.ChroMapper-PrecisionStepAdditions"; //username を各自変更してください
        public static StepAdditionController stepAdditionController;
        [Init]
        private void Init()
        {
            _harmony = new Harmony(HARMONY_ID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.sceneLoaded += SceneLoaded;
            Debug.Log("ChroMapper-PrecisionStepAdditions Plugin has loaded!");
        }

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex != 3) // Mapper scene
                return;
            if (stepAdditionController != null && stepAdditionController.isActiveAndEnabled)
                return;
            var cursorInterval = Resources.FindObjectsOfTypeAll<PrecisionStepDisplayController>().FirstOrDefault();
            if (cursorInterval == null)
                return;
            stepAdditionController = cursorInterval.gameObject.AddComponent<StepAdditionController>();
        }

        [Exit]
        private void Exit()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
            _harmony.UnpatchSelf();
            Debug.Log("ChroMapper-PrecisionStepAdditions Plugin has closed!");
        }
    }
}
