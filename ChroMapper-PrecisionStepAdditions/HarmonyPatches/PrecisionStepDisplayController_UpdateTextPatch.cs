using HarmonyLib;
using ChroMapper_PrecisionStepAdditions.Configuration;

namespace ChroMapper_PrecisionStepAdditions.HarmonyPatches
{
    [HarmonyPatch(typeof(PrecisionStepDisplayController), "UpdateText")]
    public class PrecisionStepDisplayController_UpdateTextPatch
    {
        public static bool Prefix(int newSnapping)
        {
            var currentStep = Plugin.stepAdditionController.currentStep;
            if (currentStep >= 2)
            {
                switch (currentStep)
                {
                    case 2:
                        Options.Instance.cursorPrecisionC = newSnapping;
                        break;
                    case 3:
                        Options.Instance.cursorPrecisionD = newSnapping;
                        break;
                    case 4:
                        Options.Instance.cursorPrecisionE = newSnapping;
                        break;
                }
                Plugin.stepAdditionController.additionalDisplay[currentStep - 2].text = newSnapping.ToString();
                return false;
            }
            return true;
        }
    }
}
