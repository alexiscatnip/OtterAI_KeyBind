using OpenQA.Selenium;
using System;
using System.Threading;
using System.Windows.Forms;

namespace KBOtterAI
{
    internal class Program
    {
        //sankkyuu https://github.com/gmamaladze/globalmousekeyhook/issues/3
        public static bool g_loginStatus => OtterWebInstance.getLoginStatus();

        public static bool _quit;

        public static void Main(string[] args)
        {
            ApplicationContext msgLoop = new ApplicationContext();
            
            
            string username = Environment.UserName;
            string userProfilePath = $@"C:\Users\{username}\AppData\Local\Google\Chrome\User Data";

            using (var otterWebInstance = new OtterWebInstance(userProfilePath))
            { //note: we may not be logged in at this point.  

                using (var inputManager = new InputManager())
                {
                    Thread thread = new Thread(OtterWebInstance.loopUntilDriverClose);
                    thread.Start();
                    
                    // wait until selenium window is manually closed
                    Application.Run(msgLoop);
                    // Note: we are blocked here, forever.
                    // The application actually exits from inside loopUntilDriverClose(). 
                }
            }
        }
    }
}