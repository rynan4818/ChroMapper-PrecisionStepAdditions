using HarmonyLib;

namespace ChroMapper_PrecisionStepAdditions.HarmonyPatches
{
    [HarmonyPatch(typeof(PrecisionStepDisplayController), nameof(PrecisionStepDisplayController.SelectSnap))]
    public class PrecisionStepDisplayController_SelectSnapPatch
    {
        public static bool Prefix(bool first)
        {
            if (!Plugin.stepAdditionController.init)
                return false;
            Plugin.stepAdditionController.thirdOutline.effectColor = Plugin.stepAdditionController.defaultOutlineColor;
            if (first)
                Plugin.stepAdditionController.currentStep = 0;
            else
                Plugin.stepAdditionController.currentStep = 1;
            return true;
        }
    }
}
