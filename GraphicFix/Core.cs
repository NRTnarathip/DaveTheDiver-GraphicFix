using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[assembly: MelonInfo(typeof(GraphicFix.Core), "GraphicFix", "1.0.0", "NRTnarathip", null)]
[assembly: MelonGame("nexon", "DAVE THE DIVER")]

namespace GraphicFix
{
    public class Core : MelonMod
    {
        void Log(params object[] args)
        {
            string message = string.Join(", ", args);
            LoggerInstance.Msg(message);
        }

        int maxFPS = -1;
        public override void OnInitializeMelon()
        {
            Log("Starting setup graphics...");

            Log("Original vSyncCount:", QualitySettings.vSyncCount);
            QualitySettings.vSyncCount = 0;
            Log("Fix vSyncCount:", QualitySettings.vSyncCount);

            maxFPS = Screen.currentResolution.refreshRate;
            Log("Found max fps", maxFPS);

            LoggerInstance.Msg("Initialized.");
        }

        UniversalAdditionalCameraData lastCameraData;
        public override void OnLateUpdate()
        {
            var cam = Camera.main;
            if (cam)
            {
                if (lastCameraData == null)
                {
                    lastCameraData = cam.GetComponent<UniversalAdditionalCameraData>();
                }

                if (lastCameraData?.renderPostProcessing == true)
                {
                    lastCameraData.renderPostProcessing = false;
                    Log("Fix disable post processing");
                }
            }

            if (Application.targetFrameRate != maxFPS)
            {
                Log("Original targetFrameRate", Application.targetFrameRate);
                Application.targetFrameRate = maxFPS;
                Log("Fix targetFrameRate", Application.targetFrameRate);
            }
        }
    }
}