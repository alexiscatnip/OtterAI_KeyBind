using System;
using System.Windows.Forms;
using System.Windows.Input;
using Gma.System.MouseKeyHook;

namespace KBOtterAI
{
    internal class InputManager : IDisposable
    {
        public static IKeyboardMouseEvents g_hotKeyManager;

        public InputManager()
        {
             SetUpWindowsInput();
        }
        
        public static void SetUpWindowsInput()
        {
            g_hotKeyManager = Hook.GlobalEvents();
            g_hotKeyManager.KeyDown += InputManager._hook_KeyDown;
        }

        public static void TearDownWindowsInput()
        {
            // Dispose the hotkey manager.
            g_hotKeyManager.Dispose();
        }

        public void Dispose()
        {
            TearDownWindowsInput();
            g_hotKeyManager = null;
        }

        public static void _hook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt && e.KeyCode == Keys.F5)
            {
                OtterWebInstance.Focus();
                OtterWebInstance.StartRecord_Otter();
            }

        }
    }
}