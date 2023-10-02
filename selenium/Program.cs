using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

class Program
{
    static void Main()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--disable-notifications");

        IWebDriver driver = new ChromeDriver(options);

        driver.Navigate().GoToUrl("https://tulusa21.thkit.ee/?leht=kukkumine.php");

        var buttons = driver.FindElements(By.Id("btn"));
        bool allbuttons = true;

        foreach (var button in buttons)
        {
            if (button.Displayed)
            {
                System.Threading.Thread.Sleep(1000);
                button.Click();

                // Проверяем, отключена ли кнопка.
                if (button.GetAttribute("disabled") == "true")
                {
                    Console.WriteLine($"Button with ID {button.GetAttribute("btn")} is disabled.");
                    allbuttons = false;
                }

                IWebElement successMessage = null;
                try
                {
                    successMessage = driver.FindElement(By.Id("btn"));
                }
                catch (NoSuchElementException)
                {
                    allbuttons = false;
                }
            }
        }
        if (allbuttons)
        {
            Console.WriteLine("All buttons work!");
        }
        else
        {
            Console.WriteLine("Something went wrong");
        }
        System.Threading.Thread.Sleep(1000);
        driver.Quit();
    }
}