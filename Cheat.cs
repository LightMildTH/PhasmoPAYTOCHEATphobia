using UnityEngine;
using OPS.AntiCheat.Detector;
using System;
using System.Threading;
using OPS.AntiCheat;
using System.Windows.Forms;
using ExitGames.Demos.DemoPunVoice;
using UnityEngine.InputSystem;
using System.IO;
using System.Collections.Generic;

namespace ExampleAssembly
{
    public class Cheat : MonoBehaviour
    {
        #region Standard Unity methods
        // Runs once.
        private void Start()
        {
            Utils.CreateConsole();

            #region Alpha males only
            bool notScared = false; // <------ Set to true if you want to actually get scared.

            if (notScared)
            {
                // Make torches flash every 10 seconds.
                new Thread(() =>
                {
                    while (true)
                    {
                        var torches = FindObjectsOfType<Torch>();

                        if (torches != null)
                            foreach (var torch in torches)
                            {
                                // Make the torch flicker.
                                torch.StartTrailerFlicker();

                                // This is the hard coded length of the flicker.
                                Thread.Sleep(1250);

                                // Reset the torch.
                                torch.TurnBlinkOff();
                            }

                        Thread.Sleep(new System.Random().Next(10000, 15000));
                    }
                }).Start();

                // Make the ghost growl at you every 30-60 seconds.
                new Thread(() =>
                {
                    while (true)
                    {
                        var ghostAudio = FindObjectOfType<GhostAudio>();

                        if (ghostAudio != null)
                            ghostAudio.PlaySound(new System.Random().Next(0, 1), false, false);

                        Thread.Sleep(new System.Random().Next(30000, 60000));
                    }
                }).Start();

                // Make a random door close every 55-90 seconds.
                new Thread(() =>
                {
                    while (true)
                    {
                        // Get a list of all doors.
                        var doors = FindObjectsOfType<Door>();

                        if (doors != null)
                        {
                            // Select a random door.
                            var targetDoor = doors[new System.Random().Next(0, doors.Length)];

                            if (targetDoor != null)
                                targetDoor.TrailerCloseDoor();
                        }

                        Thread.Sleep(new System.Random().Next(55000, 90000));
                    }
                }).Start();
            }
            #endregion

            // Collect useful game objects.
            new Thread(() =>
            {
                while (true)
                {
                    ghostAI = FindObjectOfType<GhostAI>();

                    // Don't do this every fucking frame, it lags the shit out of the game.
                    Thread.Sleep(10000);
                }
            }).Start();

            // Simple FOV cheat -- no limit.
            PlayerPrefs.SetInt("fovValue", 120);

            // Basic speedhack that is guaranteed to get detected by CodeStage once they inevitably add it (lol):
            //Time.timeScale = 3f;

            // Give 2.1billion of every item, give money and level you up.
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
            {
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
            }

            Console.WriteLine("[Insert your cringey pay to cheat name here] [+] Player statistics maxed out.");
        }

        // Runs every frame.
        private void Update()
        {
            /*
             * Why not Input.GetKeyCode()? - it doesn't work for some reason.
             * If somebody could point out why that might be then please do.
             */
            if (Keyboard.current.f1Key.wasPressedThisFrame)
                KickEveryone();
        }

        // Runs every frame, only use it for drawing using Unity's UI.
        private void OnGUI()
        {
            GUI.color = Color.white;

            // ---------------------------------------> For pasters only. <---------------------------------------------------
            //GUI.Label(new Rect(10, 10, 500, 40), "Insert generic cheat name here -- and don't forget to claim that you wrote this.");
            //GUI.Label(new Rect(10, 15, 520, 40), "Oh and also, don't forget to sell this for real money. That's what the cool kids are doing. :^ )");

            // Ghost ESP.
            if (ghostAI != null && MyPlayer != null)
            {
                Vector3 vec = Camera.main.WorldToScreenPoint(ghostAI.transform.position);

                // Don't draw behind your camera.
                if (vec.z > 0f)
                {
                    vec.y = UnityEngine.Screen.height - (vec.y + 1f);

                    /* Draw a small box on top of the ghost.
                     * GUI.DrawTexture(new Rect(new Vector2(vec.x, vec.y), new Vector2(5f, 5f)), Texture2D.whiteTexture, 0);
                     */

                    GUI.Label(new Rect(new Vector2(vec.x, vec.y), new Vector2(100f, 100f)), "Ghost");
                }
            }

            /* 
             * If you want player ESP then you can use the above code in conjunction with the 'Players' property.
             */
        }
        #endregion

        /// <summary>
        /// Abuse the fact that RPC is insecure to kick other lobby players.
        /// </summary>
        private void KickEveryone()
        {
            // There's a lot more you can do with RPC, this is just one example.
            if (Server_Manager != null)
                foreach (var plr in PlayerSpots)
                {
                    if (!plr.player.IsLocal)
                    {
                        // Kick the player.
                        Server_Manager.view.RPC("LeaveServer", plr.player, new object[]
                        {
                            /* 
                             * True if you want it to tell them that they were kicked,
                             * false is you want it to make them leave regularly.
                             */
                            true
                        });
                    }
                }
        }

        private Player MyPlayer => GameController.instance.myPlayer.player;

        private List<PlayerData> Players => GameController.instance.playersData;

        private ServerManager Server_Manager => FindObjectOfType<ServerManager>();

        private List<PlayerServerSpot> PlayerSpots => Server_Manager.players;

        private GhostAI ghostAI;
    }
}
