using HarmonyLib;

namespace ChroMapper_PrecisionStepAdditions.HarmonyPatches
{
    [HarmonyPatch(typeof(PrecisionStepDisplayController), nameof(PrecisionStepDisplayController.SwapSelectedInterval))]
    public class PrecisionStepDisplayController_SwapSelectedIntervalPatch
    {
        public static bool Prefix()
        {
            ++Plugin.stepAdditionController.currentStep;
            if (Plugin.stepAdditionController.currentStep > 2)
                Plugin.stepAdditionController.currentStep = 0;
            if (Plugin.stepAdditionController.currentStep == 2)
            {
                Plugin.stepAdditionController.ThirdStep();
                return false;
            }
            return true;
        }
    }
}
