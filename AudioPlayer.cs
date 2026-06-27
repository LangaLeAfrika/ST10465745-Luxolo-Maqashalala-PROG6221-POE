using System.Media;
using System;

namespace CyberSecurityBot
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.PlaySync();
            }
            catch (Exception)
            {
                Console.WriteLine("⚠ Audio file not found. Skipping voice greeting.");
            }
        }
    }
}