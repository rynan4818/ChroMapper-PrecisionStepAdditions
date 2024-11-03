using HarmonyLib;
using ChroMapper_PrecisionStepAdditions.Configuration;

namespace ChroMapper_PrecisionStepAdditions.HarmonyPatches
{
    [HarmonyPatch(typeof(PrecisionStepDisplayController), "UpdateText")]
    public class PrecisionStepDisplayController_UpdateTextPatch
    {
        public static bool Prefix(int newSnapping)
        {
            if (Plugin.stepAdditionController.currentStep == 2)
            {
                Options.Instance.cursorPrecisionC = newSnapping;
                Plugin.stepAdditionController.thirdDisplay.text = newSnapping.ToString();
                return false;
            }
            return true;
        }
    }
}
