using System;
using System.Windows.Input;
using GlobalHotKey;

namespace KBOtterAI
{
    internal class InputManager : IDisposable
    {
        public static HotKeyManager g_hotKeyManager;
        public static HotKey hotKey;

        public InputManager()
        {
            hotKey = SetUpWindowsInput();
        }
        
        

        public static HotKey SetUpWindowsInput()
        {
            
            // Create the hotkey manager.
            g_hotKeyManager = new HotKeyManager();

            // Register Ctrl+Alt+F5 hotkey. Save this variable somewhere for the further unregistering.
            hotKey = g_hotKeyManager.Register(Key.F5, ModifierKeys.Control | ModifierKeys.Alt);
            
            // Handle hotkey presses.
            g_hotKeyManager.KeyPressed += HotKeyManagerPressed;

            return hotKey;
        }

        private static void HotKeyManagerPressed(object sender, KeyPressedEventArgs e)
        {
            
            
            if (Program.g_loginStatus == false)
            {
                //error: not logged in.
                Console.WriteLine("Not logged in.");
            }

            if (e.HotKey.Key == Key.F5)
            {
                OtterWebInstance.Focus();
                OtterWebInstance.StartRecord_Otter();
            }
        }

        public static void TearDownWindowsInput()
        {
            
            // Unregister Ctrl+Alt+F5 hotkey.
            g_hotKeyManager.Unregister(hotKey);

            // Dispose the hotkey manager.
            g_hotKeyManager.Dispose();

        }

        public void Dispose()
        {
            TearDownWindowsInput();
            hotKey = null;
            g_hotKeyManager = null;
        }
    }
}