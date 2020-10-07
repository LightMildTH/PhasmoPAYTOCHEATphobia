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
using System.Collections;
using System.Linq;

namespace ExampleAssembly
{
    public class Cheat : MonoBehaviour
    {
        // Runs once.
        private void Start()
        {
            Utils.CreateConsole();

            new Thread(() =>
            {
                while (true)
                {
                    StartCoroutine(CollectGameObjects());

                    Thread.Sleep(5000);
                }
            }).Start();

            // Give 2.1billion of every item, give money and level you up.
            PlayerStats.SetMax(); 
            PlayerPrefs.SetInt("fovValue", 120);

            if (notScared)
                Console.WriteLine("[!] Disabled ghost ESP because you set notScared to true.");
        }

        // Runs every frame.
        private void Update()
        {
            /*
             * Why not Input.GetKeyCode()? - it doesn't work for some reason.
             * If somebody could point out why that might be then please do.
             */
            //if (Keyboard.current.f1Key.wasPressedThisFrame)
                //KickEveryone();

            StartCoroutine(KeyHandler());

            if (notScared)
            {
                StartCoroutine(Horror.TorchFlicker());
                StartCoroutine(Horror.GhostGrowl());
                StartCoroutine(Horror.CloseRandomDoor());
            }
        }

        // Runs every frame, only use it for drawing using Unity's UI.
        private void OnGUI()
        {
            GUI.color = Color.white;

            // Ghost ESP.
            if (ghostAI != null && MyPlayer != null && !notScared)
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

        IEnumerator CollectGameObjects()
        {
            ghostAI = FindObjectOfType<GhostAI>();

            yield return new WaitForSeconds(0.15f);

            serverManager = FindObjectOfType<ServerManager>();

            yield return new WaitForSeconds(0.15f);

            torches = FindObjectsOfType<Torch>().ToList();

            yield return new WaitForSeconds(0.15f);

            ghostAudio = FindObjectOfType<GhostAudio>();

            yield return new WaitForSeconds(0.15f);

            doors = FindObjectsOfType<Door>().ToList();

            yield return null;
        }

        IEnumerator KeyHandler()
        {
            var keyboard = Keyboard.current;

            if (keyboard.f10Key.wasPressedThisFrame)
                KickEveryone();
            
            if (keyboard.f1Key.wasPressedThisFrame)
            {
                CheatToggles.speedhack = !CheatToggles.speedhack;

                if (CheatToggles.speedhack)
                    Time.timeScale = 3f;
                else
                    Time.timeScale = 1f;
            }

            yield return new WaitForEndOfFrame();
        }

        /// <summary>
        /// Abuse the fact that RPC is insecure to kick other lobby players.
        /// </summary>
        private void KickEveryone()
        {
            // There's a lot more you can do with RPC, this is just one example.
            if (serverManager != null)
                foreach (var plr in PlayerSpots)
                {
                    if (!plr.player.IsLocal)
                    {
                        // Kick the player.
                        serverManager.view.RPC("LeaveServer", plr.player, new object[]
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

        
        public static bool notScared = false; // <------ Set to true if you want to actually get scared.

        private Player MyPlayer => GameController.instance.myPlayer.player;

        private List<PlayerData> Players => GameController.instance.playersData;

        public static ServerManager serverManager;

        private List<PlayerServerSpot> PlayerSpots => serverManager.players;

        public static GhostAI ghostAI;

        public static GhostAudio ghostAudio;

        public static List<Torch> torches;

        public static List<Door> doors;
    }
}
