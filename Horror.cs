using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExampleAssembly
{
    public class Horror : MonoBehaviour
    {
        public static IEnumerator TorchFlicker()
        {
            yield return new WaitForSeconds(new System.Random().Next(15, 30));

            if (Cheat.torches == null)
                yield return null;

            foreach (var torch in Cheat.torches)
            {
                // Make the torch flicker.
                torch.StartTrailerFlicker();

                // This is the hard coded length of the flicker.
                yield return new WaitForSeconds(1.25f);

                // Reset the torch.
                torch.TurnBlinkOff();
            }

            //if (Cheat.notScared)
                //StartCoroutine(TorchFlicker());
        }

        public static IEnumerator GhostGrowl()
        {
            yield return new WaitForSeconds(new System.Random().Next(70, 120));

            if (Cheat.ghostAudio == null)
                yield return null;

            // The only two valid growl IDs are 0 and 1 (that I've managed to find).
            Cheat.ghostAudio.PlaySound(new System.Random().Next(0, 1), false, false);

            //if (Cheat.notScared)
                //StartCoroutine(GhostGrowl());
        }

        public static IEnumerator CloseRandomDoor()
        {
            yield return new WaitForSeconds(new System.Random().Next(55, 90));

            if (Cheat.doors == null)
                yield return null;

            // Select a random door.
            var targetDoor = Cheat.doors[new System.Random().Next(0, Cheat.doors.Count)];

            // Close the door.
            if (targetDoor != null)
                targetDoor.TrailerCloseDoor();

            //if (Cheat.notScared)
                //StartCoroutine(CloseRandomDoor());
        }
    }
}
