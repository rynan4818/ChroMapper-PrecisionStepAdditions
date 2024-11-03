using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using HarmonyLib;
using ChroMapper_PrecisionStepAdditions.Configuration;

namespace ChroMapper_PrecisionStepAdditions
{
    public class StepAdditionController : MonoBehaviour
    {
        private PrecisionStepDisplayController precisionStepDisplayController;
        private Traverse firstActive;
        private Outline firstOutline;
        private Outline secondOutline;
        private Color selectedOutlineColor;
        public Color defaultOutlineColor { get; set; }
        public TMP_InputField thirdDisplay { get; set; }
        public Outline thirdOutline { get; set; }
        public int currentStep { set; get; } = 0;
        public bool init { get; set; } = false;
        private void Start()
        {
            this.precisionStepDisplayController = this.gameObject.GetComponent<PrecisionStepDisplayController>();
            var psdcTraverse = Traverse.Create(this.precisionStepDisplayController);
            this.firstActive = psdcTraverse.Field("firstActive");
            this.firstOutline = psdcTraverse.Field("firstOutline").GetValue<Outline>();
            this.secondOutline = psdcTraverse.Field("secondOutline").GetValue<Outline>();
            this.selectedOutlineColor = psdcTraverse.Field("selectedOutlineColor").GetValue<Color>();
            this.defaultOutlineColor = psdcTraverse.Field("defaultOutlineColor").GetValue<Color>();

            // Third Intervalの作成
            var secondInterval = this.transform.Find("Second Interval");
            var thirdDInterval = Instantiate(secondInterval, this.transform);
            thirdDInterval.name = "Third Interval";
            this.thirdDisplay = thirdDInterval.GetComponent<TMP_InputField>();
            this.thirdOutline = thirdDInterval.Find("Background").GetComponent<Outline>();
            for (int i = 0; i < this.thirdDisplay.onSelect.GetPersistentEventCount(); i++)
            {
                //https://docs.unity3d.com/ja/2018.4/ScriptReference/UI.InputField.SubmitEvent.html
                if (this.thirdDisplay.onSelect.GetPersistentMethodName(i) == "SelectSnap")
                    this.thirdDisplay.onSelect.SetPersistentListenerState(i, UnityEventCallState.Off);
            }
            this.thirdDisplay.onSelect.AddListener((s) => this.ThirdStep());

            this.thirdDisplay.text = Options.Instance.cursorPrecisionC.ToString();
            this.init = true;
            this.precisionStepDisplayController.SelectSnap(true);
        }
        public void ThirdStep()
        {
            this.firstActive.SetValue(false);
            this.currentStep = 2;
            this.firstOutline.effectColor = this.defaultOutlineColor;
            this.secondOutline.effectColor = this.defaultOutlineColor;
            this.thirdOutline.effectColor = this.selectedOutlineColor;
            this.precisionStepDisplayController.UpdateManualPrecisionStep(this.thirdDisplay.text);
        }
        private void OnDestroy()
        {
            Options.Instance.SettingSave();
        }
    }
}
