using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace WClickSwapper
{
    public class ModEntry : Mod
    {
        private bool isRightClickEnabled = false;

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady) return;

            // Toggle mode with 'W'
            if (e.Button == SButton.W)
            {
                isRightClickEnabled = !isRightClickEnabled;
                string status = isRightClickEnabled ? "ENABLED" : "DISABLED";
                Game1.addHUDMessage(new HUDMessage($"Right-Click Mode: {status}", 3));
                
                // Prevent 'W' from making you walk up while toggling
                this.Helper.Input.Suppress(SButton.W);
            }

            // If active, turn Left-Click (tap) into Right-Click
            if (isRightClickEnabled && e.Button == SButton.MouseLeft)
            {
                this.Helper.Input.Suppress(SButton.MouseLeft);
                this.Helper.Input.TriggerMouseButton(SButton.MouseRight);
            }
        }
    }
}
