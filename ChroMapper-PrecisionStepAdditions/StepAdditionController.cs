using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using HarmonyLib;
using ChroMapper_PrecisionStepAdditions.Configuration;
using System.Collections.Generic;

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
        public List<TMP_InputField> additionalDisplay { get; set; } = new List<TMP_InputField>();
        public List<Outline> additionalOutline { get; set; } = new List<Outline>();
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
            if (Options.Instance.additionalStep > 3)
                Options.Instance.additionalStep = 3;
            if (Options.Instance.additionalStep < 0)
                Options.Instance.additionalStep = 0;
            this.additionalUI();
            this.init = true;
            this.precisionStepDisplayController.SelectSnap(true);
        }

        private void additionalUI()
        {
            var secondInterval = this.secondOutline.transform.parent;
            var size = secondInterval.GetComponent<RectTransform>().sizeDelta;
            if (Options.Instance.additionalStep >= 1)
            {
                if (Options.Instance.additionalStep == 1)
                    size.y = 13f;
                else if (Options.Instance.additionalStep == 2)
                    size.y = 9f;
                else if (Options.Instance.additionalStep == 3)
                    size.y = 7f;
                this.firstOutline.transform.parent.GetComponent<RectTransform>().sizeDelta = size;
                secondInterval.GetComponent<RectTransform>().sizeDelta = size;
            }
            for (int j = 0; j < Options.Instance.additionalStep; j++)
            {
                // Third Intervalの作成
                var additionalInterval = Instantiate(secondInterval, this.transform);
                additionalInterval.name = $"Interval{j + 3}";
                additionalInterval.GetComponent<RectTransform>().sizeDelta = size;
                this.additionalDisplay.Add(additionalInterval.GetComponent<TMP_InputField>());
                this.additionalOutline.Add(additionalInterval.Find("Background").GetComponent<Outline>());
                for (int i = 0; i < this.additionalDisplay[j].onSelect.GetPersistentEventCount(); i++)
                {
                    //https://docs.unity3d.com/ja/2018.4/ScriptReference/UI.InputField.SubmitEvent.html
                    if (this.additionalDisplay[j].onSelect.GetPersistentMethodName(i) == "SelectSnap")
                        this.additionalDisplay[j].onSelect.SetPersistentListenerState(i, UnityEventCallState.Off);
                }
                switch (j)
                {
                    case 0:
                        this.additionalDisplay[j].onSelect.AddListener((s) => this.ThirdStep());
                        this.additionalDisplay[j].text = Options.Instance.cursorPrecisionC.ToString();
                        break;
                    case 1:
                        this.additionalDisplay[j].onSelect.AddListener((s) => this.FourthStep());
                        this.additionalDisplay[j].text = Options.Instance.cursorPrecisionD.ToString();
                        break;
                    case 2:
                        this.additionalDisplay[j].onSelect.AddListener((s) => this.FifthStep());
                        this.additionalDisplay[j].text = Options.Instance.cursorPrecisionE.ToString();
                        break;
                }
            }
        }

        public void ThirdStep()
        {
            this.AdditionalSet();
            this.currentStep = 2;
            if (Options.Instance.additionalStep > 0)
                this.additionalOutline[0].effectColor = this.selectedOutlineColor;
            if (Options.Instance.additionalStep > 1)
                this.additionalOutline[1].effectColor = this.defaultOutlineColor;
            if (Options.Instance.additionalStep > 2)
                this.additionalOutline[2].effectColor = this.defaultOutlineColor;
            this.precisionStepDisplayController.UpdateManualPrecisionStep(this.additionalDisplay[0].text);
        }
        public void FourthStep()
        {
            this.AdditionalSet();
            this.currentStep = 3;
            if (Options.Instance.additionalStep > 0)
                this.additionalOutline[0].effectColor = this.defaultOutlineColor;
            if (Options.Instance.additionalStep > 1)
                this.additionalOutline[1].effectColor = this.selectedOutlineColor;
            if (Options.Instance.additionalStep > 2)
                this.additionalOutline[2].effectColor = this.defaultOutlineColor;
            this.precisionStepDisplayController.UpdateManualPrecisionStep(this.additionalDisplay[1].text);
        }
        public void FifthStep()
        {
            this.AdditionalSet();
            this.currentStep = 4;
            if (Options.Instance.additionalStep > 0)
                this.additionalOutline[0].effectColor = this.defaultOutlineColor;
            if (Options.Instance.additionalStep > 1)
                this.additionalOutline[1].effectColor = this.defaultOutlineColor;
            if (Options.Instance.additionalStep > 2)
                this.additionalOutline[2].effectColor = this.selectedOutlineColor;
            this.precisionStepDisplayController.UpdateManualPrecisionStep(this.additionalDisplay[2].text);
        }
        private void AdditionalSet()
        {
            this.firstActive.SetValue(false);
            this.firstOutline.effectColor = this.defaultOutlineColor;
            this.secondOutline.effectColor = this.defaultOutlineColor;
        }
        private void OnDestroy()
        {
            for (int i = 0; i < Options.Instance.additionalStep; i++)
                this.additionalDisplay[i].onSelect.RemoveAllListeners();
            Options.Instance.SettingSave();
        }
    }
}
