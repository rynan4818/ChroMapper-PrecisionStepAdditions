using ChroMapper_PrecisionStepAdditions.Configuration;
using HarmonyLib;

namespace ChroMapper_PrecisionStepAdditions.HarmonyPatches
{
    [HarmonyPatch(typeof(PrecisionStepDisplayController), nameof(PrecisionStepDisplayController.SwapSelectedInterval))]
    public class PrecisionStepDisplayController_SwapSelectedIntervalPatch
    {
        public static bool Prefix()
        {
            ++Plugin.stepAdditionController.currentStep;
            if (Plugin.stepAdditionController.currentStep > 1 + Options.Instance.additionalStep)
                Plugin.stepAdditionController.currentStep = 0;
            if (Plugin.stepAdditionController.currentStep >= 2)
            {
                switch (Plugin.stepAdditionController.currentStep)
                {
                    case 2:
                        Plugin.stepAdditionController.ThirdStep();
                        break;
                    case 3:
                        Plugin.stepAdditionController.FourthStep();
                        break;
                    case 4:
                        Plugin.stepAdditionController.FifthStep();
                        break;
                }
                return false;
            }
            return true;
        }
    }
}
