using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace KBOtterAI
{
    public class OtterWebInstance : IDisposable
    {
        public static IWebDriver g_driver;
        public OtterWebInstance(string userProfilePath)
        {
            var path = "C:\\workspace\\selenium_automation\\chromedriver.exe";

            // Set up Selenium ChromeDriver
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--title=OtterAI Keyboard Helper"); // Maximize the browser window
            chromeOptions.AddArgument("--window-size=500,500"); 
            chromeOptions.AddArgument($"--user-data-dir={userProfilePath}");
            g_driver = new ChromeDriver(path, chromeOptions);

            //global waiting of 10s for each click/read.
            // g_driver.Manage().Timeouts().ImplicitWait = 
            //     TimeSpan.FromSeconds(10);
            
            // Navigate to the Otter.ai website
            g_driver.Navigate().GoToUrl("https://otter.ai/home");
            // Wait until the page is fully loaded
            WebDriverWait wait = new WebDriverWait(g_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            
            return;
        }

        #region Login
        
        //context: /home
        // 
        // string "Not Logged In" is present  -> return false
        // else  -> return true
        private static Boolean home_IsLoggedIn()
        {
            // Check if the string "Not Logged In" is present in the page source
            if (g_driver.PageSource.Contains("Not Logged In"))
            {
                Console.WriteLine("logged in.");
                return true;
            }
            else
            {
                Console.WriteLine("not logged in ");
                return false;
            }
        }        
        
        #endregion

        
        #region Recording
        
        public static void StartRecord_Otter()
        {
            try
            {
                var button =
                    g_driver.FindElement(By.CssSelector("button[mat-button][class*=record][class*=mat-button]"));
                button.Click();
            }
            catch (NoSuchElementException e)
            {
                //print("Unable to locate the record button.")
            }
        }
        #endregion

        public static void Focus()
        {
            //bring up to top
            g_driver.SwitchTo().Window(g_driver.CurrentWindowHandle);
        }

        public static void loopUntilDriverClose()
        {
            try
            {
                if (g_driver.Title == "")
                    return;
                Thread.Sleep(200);
            }
            catch (Exception e)
            {
                return;
            }
        }
        
        
        public void Dispose()
        {
            TearDownOtterWindow();
        }

        public static void TearDownOtterWindow()
        {
            g_driver.Quit();
            g_driver.Dispose();
        }

        public static bool getLoginStatus()
        {
            return home_IsLoggedIn();

        }
    }
}