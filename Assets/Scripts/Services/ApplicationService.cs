using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


    public class ApplicationService : BaseService<ApplicationService>
    {
        public override void Initialize()
        {
            base.Initialize();
            SetFrameRate();
            DOTween.Init();
            DOTween.SetTweensCapacity(1000, 100);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void SetFrameRate()
        {
            if (SystemInfo.processorCount == 1 && Screen.width <= 600)
            {
                Application.targetFrameRate = 30;
            }
            else
            {
                Application.targetFrameRate = 60;
            }
        }
    }
