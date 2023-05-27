using OpenQA.Selenium;
using System;
using System.Windows.Input;
using System.Windows.Threading;
using GlobalHotKey;

namespace KBOtterAI
{
    internal class Program
    {
        public static bool g_loginStatus => OtterWebInstance.getLoginStatus();

        [STAThread] // -- required for `new HotKeyManager();`
        public static void Main(string[] args)
        {
            string username = Environment.UserName;
            string userProfilePath = $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data";

            using (var otterWebInstance = new OtterWebInstance(userProfilePath))
            { //note: we may not be logged in at this point.  

                using (var inputManager = new InputManager())
                {
                    //wait until selenium window is manually closed
                    
                    // TODO: what the hecking https://github.com/kyrylomyr/GlobalHotKey/issues/2
                    System.Windows.Threading.Dispatcher.Run();

                    OtterWebInstance.loopUntilDriverClose();

                    Dispatcher.CurrentDispatcher.InvokeShutdown();
                }
            }
        }
    }
}