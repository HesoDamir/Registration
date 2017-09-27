using ElementWaiting;
using Logger;
using Login;
using System;
using System.Media;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System.Net;

namespace Registration
{
    public class TestCaseRegistration : Constants
    {
        const string messgWE = "Невозможно соединиться с удаленным сервером. Возможно, удаленный компьютер не был запушен.";
        const string messgNSEE = "Элемент не найден, проверьте корректность локатора.";
        const string messgIOE = "Элемент некликабельный, возможно обработчик событий элемента не успел подгрузится.";
        const string messgEx = "Что-то пошло не так.";
        const string messgDone = "Тест пройден успешно.";
        const string captErr = "Ошибка";
        const string captDone = "Готово";
        IWebDriver driver;
        const MessageBoxButtons btnBoxOK = MessageBoxButtons.OK;
        const MessageBoxIcon icBoxErr = MessageBoxIcon.Error;
        const MessageBoxIcon icBoxInf = MessageBoxIcon.Information;
        const MessageBoxOptions optBoxSN = MessageBoxOptions.ServiceNotification;
        public void UserReg(string selBrw)
        {
            try
            {
                switch (selBrw)
                {
                    case "Chrome":
                        driver = new ChromeDriver(@"BrowserDrivers/");
                        break;
                    case "Firefox":
                        Environment.SetEnvironmentVariable("webdriver.gecko.driver", "geckodriver.exe");
                        driver = new FirefoxDriver();
                        break;
                    case "IE":
                        driver = new InternetExplorerDriver(@"BrowserDrivers/");
                        break;
                    case "RemoteChrome":
                        DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                        driver = new RemoteWebDriver(new Uri("http://192.168.0.94:5555/wd/hub"), capabilities);
                        break;
                }
                driver.Navigate().GoToUrl(baseUrl);
                System.Threading.Thread.Sleep(1000);

                RegFD.RegUsingBtnEnter(driver);

                Log.WriteArg(endLog);
                SystemSounds.Asterisk.Play();
                MessageBox.Show(messgDone, captDone, btnBoxOK, icBoxInf, 0, optBoxSN);
                Console.ReadKey();
                driver.Quit();
            }
            catch (NoSuchElementException ex)
            {
                Log.Write(ex);
                Log.WriteArg(endLog);
                Log.TakeScreenshot(driver);
                SystemSounds.Hand.Play();
                MessageBox.Show(messgNSEE, captErr, btnBoxOK, icBoxErr, 0, optBoxSN);
            }
            catch (WebDriverException ex)
            {
                Log.Write(ex);
                SystemSounds.Hand.Play();
                MessageBox.Show(messgWE, captErr, btnBoxOK, icBoxErr, 0, optBoxSN);
            }
            catch (InvalidOperationException ex)
            {
                Log.Write(ex);
                Log.WriteArg(endLog);
                Log.TakeScreenshot(driver);
                SystemSounds.Hand.Play();
                MessageBox.Show(messgIOE, captErr, btnBoxOK, icBoxErr, 0, optBoxSN);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Log.WriteArg(endLog);
                Log.TakeScreenshot(driver);
                SystemSounds.Hand.Play();
                MessageBox.Show(messgEx, captErr, btnBoxOK, icBoxErr, 0, optBoxSN);
            }
        }
    }

    class MainClass
    {
        static void Main(string[] args)
        {
            string selBrw = string.Empty;
            if (args.Length == 1)
            {
                selBrw = args[0];
            }
            else
            {
                selBrw = "Chrome";
            }
            TestCaseRegistration tcr = new TestCaseRegistration();
            tcr.UserReg(selBrw);
        }
    }
}