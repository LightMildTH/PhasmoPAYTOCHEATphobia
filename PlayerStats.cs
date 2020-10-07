using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAssembly
{
    public class PlayerStats
    {
        public static void SetMax()
        {
            /* 
             * Yes, I am aware that this is a shit way to do it. I XORed the file contents -
             * and wrote the data automatically but this adds a lot more customisation.
             * 
             * If you would like to write that yourself then look no further -
             * than the 'DataScrambler' method inside FileBasedPrefs.cs. 
             *
             * The only other useful keys that aren't included below are:
             * - "MissionStatus"
             * - "setupPhase"
             * - "StayInServerRoom"
             * - "isTutorial"
             * - "LevelDifficulty"
             */

            // Player stats
            FileBasedPrefs.SetInt("myTotalExp", int.MaxValue);
            FileBasedPrefs.SetInt("totalExp", int.MaxValue);
            FileBasedPrefs.SetInt("PlayersMoney", int.MaxValue);
            FileBasedPrefs.SetInt("completedTraining", 1);
            FileBasedPrefs.SetInt("PlayerDied", 0);

            // Items
            FileBasedPrefs.SetInt("EMFReaderInventory", int.MaxValue);
            FileBasedPrefs.SetInt("FlashlightInventory", int.MaxValue);
            FileBasedPrefs.SetInt("CameraInventory", int.MaxValue);
            FileBasedPrefs.SetInt("LighterInventory", int.MaxValue);
            FileBasedPrefs.SetInt("CandleInventory", int.MaxValue);
            FileBasedPrefs.SetInt("UVFlashlightInventory", int.MaxValue);
            FileBasedPrefs.SetInt("CrucifixInventory", int.MaxValue);
            FileBasedPrefs.SetInt("DSLRCameraInventory", int.MaxValue);
            FileBasedPrefs.SetInt("EVPRecorderInventory", int.MaxValue);
            FileBasedPrefs.SetInt("SaltInventory", int.MaxValue);
            FileBasedPrefs.SetInt("TripodInventory", int.MaxValue);
            FileBasedPrefs.SetInt("StrongFlashlightInventory", int.MaxValue);
            FileBasedPrefs.SetInt("MotionSensorInventory", int.MaxValue);
            FileBasedPrefs.SetInt("SoundSensorInventory", int.MaxValue);
            FileBasedPrefs.SetInt("SanityPillsInventory", int.MaxValue);
            FileBasedPrefs.SetInt("ThermometerInventory", int.MaxValue);
            FileBasedPrefs.SetInt("GhostWritingBookInventory", int.MaxValue);
            FileBasedPrefs.SetInt("IRLightSensorInventory", int.MaxValue);
            FileBasedPrefs.SetInt("ParabolicMicrophoneInventory", int.MaxValue);
            FileBasedPrefs.SetInt("IRLightSensorInventory", int.MaxValue);
            FileBasedPrefs.SetInt("ParabolicMicrophoneInventory", int.MaxValue);
            FileBasedPrefs.SetInt("GlowstickInventory", int.MaxValue);
            FileBasedPrefs.SetInt("HeadMountedCameraInventory", int.MaxValue);

            Console.WriteLine("[+] Player statistics maxed out.");
        }
    }
}
