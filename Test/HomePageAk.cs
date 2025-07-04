using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using System.Threading;


namespace RecodeSolutions.Tests
{
    [TestFixture]
    public class HomePageAk
    {
        private IWebDriver driver;
        private Actions action;
        private IJavaScriptExecutor js;
        private WebDriverWait wait;
        private string originalWindow;
        private ExtentReports extent;
        private ExtentTest test;
        //         private ExtentReports extent;
        // private ExtentTest test;

        private string CaptureScreenshot(string fileName)
        {
            try
            {
                string reportDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Reports");
                string screenshotDir = Path.Combine(reportDir, "Screenshots");
                Directory.CreateDirectory(screenshotDir);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");  // ✅ Store timestamp once
                string fileNameWithTimestamp = $"{fileName}_{timestamp}.png";

                string filePath = Path.Combine(screenshotDir, fileNameWithTimestamp);
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath); // Saves as PNG by default

                return Path.Combine("Screenshots", fileNameWithTimestamp);  // ✅ Return relative path
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Failed to capture screenshot: " + ex.Message);
                return string.Empty;
            }
        }



        [OneTimeSetUp]
        public void Setup()
        {
            // Create directories for reports and screenshots
            string reportDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Reports");
            string screenshotDir = Path.Combine(reportDir, "Screenshots");

            Directory.CreateDirectory(reportDir);
            Directory.CreateDirectory(screenshotDir);

            // Setup ExtentReports
            string reportPath = Path.Combine(reportDir, "ExtentReport.html");
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.DocumentTitle = "Recode Solutions - Test Report";
            htmlReporter.Config.ReportName = "Recode UI Automation Suite";

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            // Chrome browser options
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--remote-allow-origins=*");

            // Launch Chrome
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.recodesolutions.com");

            // Initialize utility objects
            action = new Actions(driver);
            js = (IJavaScriptExecutor)driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Console.WriteLine("✅ Browser launched and Recode Solutions website loaded.");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                    Console.WriteLine("✅ Browser closed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error during driver shutdown: " + ex.Message);
            }

            extent.Flush();
            Console.WriteLine("📄 Extent report generated.");
        }


        [Test, Order(1)]
        public void TC01_HoverOnAboutUs()
        {
            test = extent.CreateTest("TC01_HoverOnAboutUs", "Hover over the 'About Us' menu item");

            try
            {
                var about = driver.FindElement(By.XPath("//li[@id='menu-item-12159']//span[text()='About Us']"));
                test.Log(Status.Info, "Located 'About Us' menu item.");

                action.MoveToElement(about).Perform();
                test.Log(Status.Pass, "'About Us' hover action performed successfully.");

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC01_HoverOnAboutUs");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(2)]
        public void TC02_HoverOnSolutions()
        {
            test = extent.CreateTest("TC02_HoverOnSolutions", "Hover over the 'Solutions' menu item");

            try
            {
                var solutions = driver.FindElement(By.XPath("//li[ul/li[@id='menu-item-12266']]//span[text()='Solutions']"));
                test.Log(Status.Info, "Located 'Solutions' menu item.");

                action.MoveToElement(solutions).Perform();
                test.Log(Status.Pass, "'Solutions' hover action performed successfully.");

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC02_HoverOnSolutions");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(3)]
        public void TC03_HoverOnPlatforms()
        {
            test = extent.CreateTest("TC03_HoverOnPlatforms", "Hover over the 'Platforms' menu item");

            try
            {
                var platforms = driver.FindElement(By.XPath("(//span[@class='pxl-menu-item-text' and text()='Platforms'])[1]"));
                test.Log(Status.Info, "Located 'Platforms' menu item.");

                action.MoveToElement(platforms).Perform();
                test.Log(Status.Pass, "'Platforms' hover action performed successfully.");

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC03_HoverOnPlatformss");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(4)]
        public void TC04_HoverOnCareers()
        {
            test = extent.CreateTest("TC04_HoverOnCareers", "Hover over the 'Careers' menu item");

            try
            {
                var careers = driver.FindElement(By.XPath("//li[ul[@class='sub-menu'] and .//span[text()='Careers']]//span[text()='Careers']"));
                test.Log(Status.Info, "Located 'Careers' menu item.");

                action.MoveToElement(careers).Perform();
                test.Log(Status.Pass, "'Careers' hover action performed successfully.");

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC04_HoverOnCareers");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(5)]
        public void TC05_VerifyLogoAndText()
        {
            test = extent.CreateTest("TC05_VerifyLogoAndText", "Verify Recode logo and its adjacent text");

            try
            {
                var element = driver.FindElement(By.XPath("//sr7-txt[@id='SR7_1_1-2-1']"));
                string elementText = element.Text;
                test.Log(Status.Info, $"Text near logo found: {elementText}");

                var logo = driver.FindElement(By.XPath("(//img[contains(@src, 'recode-logo-final.png')])[1]"));
                bool isDisplayed = logo.Displayed;
                test.Log(Status.Info, "Logo display status: " + isDisplayed);

                if (isDisplayed)
                {
                    test.Log(Status.Pass, "✅ Recode logo is displayed correctly.");
                }
                else
                {
                    test.Log(Status.Fail, "❌ Recode logo is not displayed.");
                }

                Console.WriteLine("Element Text: " + elementText);
                Console.WriteLine("Logo Displayed: " + isDisplayed);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC05_VerifyLogoAndText");
                test.Log(Status.Fail, $"❌ Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(6)]
        public void TC06_NavigateToDeployDigitalWorkers()
        {
            test = extent.CreateTest("TC06_NavigateToDeployDigitalWorkers", "Navigate to 'Deploy Digital Workers' section");

            try
            {
                var target = wait.Until(driver => driver.FindElement(By.XPath("//span[text()='Deploy Digital Workers']")));
                test.Log(Status.Info, "'Deploy Digital Workers' element located.");

                js.ExecuteScript("arguments[0].scrollIntoView(true);", target);
                test.Log(Status.Pass, "'Deploy Digital Workers' section scrolled into view.");

                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC06_NavigateToDeployDigitalWorkers");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }



        [Test, Order(7)]
        public void TC07_VerifyAboutUsSection()
        {
            test = extent.CreateTest("TC07_VerifyAboutUsSection", "Verify 'About Us' section");
            try
            {
                js.ExecuteScript("window.scrollBy(0, 1000);");
                Thread.Sleep(2000);
                var aboutus = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div/div/div/main/article/div/div/section[2]/div/div/div/div"));
                test.Log(Status.Info, "About Us section found.");
                Console.WriteLine("About Us: " + aboutus.Text);
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"Test failed: {ex.Message}");
                throw;
            }
        }

        [Test, Order(8)]
        public void TC08_CheckOurStory()
        {
            test = extent.CreateTest("TC08_CheckOurStory", "Click on 'Our Story' section to verify it's functional");

            try
            {
                var storyBtn = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/main/article/div/div/section[2]/div/div/div/section/div/div[3]/div/div/div/div/div/div[1]/span[1]"));
                storyBtn.Click();
                Thread.Sleep(2000);

                test.Log(Status.Pass, "Clicked on 'Our Story' button successfully.");
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"Failed to click on 'Our Story': {ex.Message}");
                throw;
            }
        }


        [Test, Order(9)]
        public void TC09_VerifyStoryAndValuesDetails()
        {
            test = extent.CreateTest("TC09_VerifyStoryAndValuesDetails", "Verify the 'Our Story' and 'Our Values' sections display correct content.");

            try
            {
                var storyText = driver.FindElement(By.XPath("//section//h3/span")).Text;
                Console.WriteLine("Story: " + storyText);
                test.Log(Status.Pass, $"Story section text: {storyText}");

                var valuesBtn = driver.FindElement(By.XPath("//span[normalize-space()='Our Values']"));
                valuesBtn.Click();
                test.Log(Status.Info, "'Our Values' button clicked.");

                var valuesText = driver.FindElement(By.XPath("//section//h3")).Text;
                Console.WriteLine("Values: " + valuesText);
                test.Log(Status.Pass, $"Values section text: {valuesText}");
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"Test failed: {ex.Message}");
                throw;
            }
        }


        // Continue similarly for TC10 to TC34...


        [Test, Order(10)]
        public void TC10_CheckServicesHeading()
        {
            test = extent.CreateTest("TC10_CheckServicesHeading", "Verify the Services section heading is displayed correctly.");

            try
            {
                // Scroll to service heading
                js.ExecuteScript("window.scrollBy(0, 1000);");
                test.Log(Status.Info, "Scrolled down to locate the Services heading.");

                // Locate the service heading
                var services = driver.FindElement(By.XPath("//section[3]//h3"));
                string headingText = services.Text;
                Console.WriteLine("Service Heading: " + headingText);

                test.Log(Status.Pass, $"Service heading found: {headingText}");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC10_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(11)]
        public void TC11_NavigateToAILedAutomation()
        {
            test = extent.CreateTest("TC11_NavigateToAILedAutomation", "Click on AI-Led Automation link and verify heading.");

            try
            {
                var aiLedLink = driver.FindElement(By.XPath("//section[3]//a"));
                aiLedLink.Click();
                test.Log(Status.Info, "Clicked on AI-Led Automation section link.");
                Thread.Sleep(2000);

                var heading = driver.FindElement(By.XPath("//section//h3/span")).Text;
                test.Log(Status.Pass, $"AI-Led Page Heading: {heading}");
                Console.WriteLine("AI-Led Page Heading: " + heading);

                var logo = driver.FindElement(By.XPath("(//img[contains(@src, 'recode-logo-final.png')])[1]"));
                logo.Click();
                test.Log(Status.Info, "Clicked on Recode logo to return to home page.");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC11_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(12)]
        public void TC12_VerifyGENAIService()
        {
            test = extent.CreateTest("TC12_VerifyGENAIService", "Verify the presence of GEN AI Service section");

            try
            {
                js.ExecuteScript("window.scrollBy(0, 2000);");
                test.Log(Status.Info, "Scrolled down by 2000 pixels.");

                var service = driver.FindElement(By.XPath("(//div[@class=\"pxl-item--holder \"]//h3)[1]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", service);
                test.Log(Status.Info, "Scrolled into view of the GEN AI Service element.");
                Thread.Sleep(1000);

                string serviceText = service.Text;
                Console.WriteLine("Service provided: " + serviceText);
                test.Log(Status.Pass, $"GEN AI Service found with text: {serviceText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC12_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        [Test, Order(13)]
        public void TC13_VerifyAILedAutomationService()
        {
            test = extent.CreateTest("TC13_VerifyAILedAutomationService", "Verify the presence of AI-Led Automation Service section");

            try
            {
                var service = driver.FindElement(By.XPath("(//div[@class='pxl-item--holder ']//h3)[2]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", service);
                test.Log(Status.Info, "Scrolled to AI-Led Automation Service section.");
                Thread.Sleep(1000);

                string serviceText = service.Text;
                Console.WriteLine("Service provided: " + serviceText);
                test.Log(Status.Pass, $"AI-Led Automation Service found with text: {serviceText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC13_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(14)]
        public void TC14_VerifyDataAnalyticsService()
        {
            test = extent.CreateTest("TC14_VerifyDataAnalyticsService", "Verify the presence of Data Analytics Service section");

            try
            {
                var service = driver.FindElement(By.XPath("(//div[@class='pxl-item--holder ']//h3)[3]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", service);
                test.Log(Status.Info, "Scrolled to Data Analytics Service section.");
                Thread.Sleep(1000);

                string serviceText = service.Text;
                Console.WriteLine("Service provided: " + serviceText);
                test.Log(Status.Pass, $"Data Analytics Service found with text: {serviceText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC14_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        [Test, Order(15)]
        public void TC15_VerifyIntegrationServices()
        {
            test = extent.CreateTest("TC15_VerifyIntegrationServices", "Verify the presence of Integration Services section");

            try
            {
                var service = driver.FindElement(By.XPath("(//div[@class='pxl-item--holder ']//h3)[4]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", service);
                test.Log(Status.Info, "Scrolled to Integration Services section.");
                Thread.Sleep(1000);

                string serviceText = service.Text;
                Console.WriteLine("Service provided: " + serviceText);
                test.Log(Status.Pass, $"Integration Service found with text: {serviceText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC15_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(16)]
        public void TC16_VerifyDigitalCommerceSolutions()
        {
            test = extent.CreateTest("TC16_VerifyDigitalCommerceSolutions", "Verify the presence of Digital Commerce Solutions section");

            try
            {
                var service = driver.FindElement(By.XPath("(//div[@class='pxl-item--holder ']//h3)[5]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", service);
                test.Log(Status.Info, "Scrolled to Digital Commerce Solutions section.");
                Thread.Sleep(1000);

                string serviceText = service.Text;
                Console.WriteLine("Service provided: " + serviceText);
                test.Log(Status.Pass, $"Digital Commerce Solution found with text: {serviceText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC16_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(17)]
        public void TC17_VerifyQualityAssuranceServices()
        {
            test = extent.CreateTest("TC17_VerifyQualityAssuranceServices", "Verify the presence of Quality Assurance Services section");

            try
            {
                var service = driver.FindElement(By.XPath("(//div[@class='pxl-item--holder ']//h3)[6]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", service);
                test.Log(Status.Info, "Scrolled to Quality Assurance Services section.");
                Thread.Sleep(1000);

                string serviceText = service.Text;
                Console.WriteLine("Service provided: " + serviceText);
                test.Log(Status.Pass, $"Quality Assurance Service found with text: {serviceText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC17_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(18)]
        public void TC18_VerifyDevOpsService()
        {
            test = extent.CreateTest("TC18_VerifyDevOpsService", "Verify the presence of DevOps Service section");

            try
            {
                var service = driver.FindElement(By.XPath("(//div[@class='pxl-item--holder ']//h3)[7]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", service);
                test.Log(Status.Info, "Scrolled to DevOps Service section.");
                Thread.Sleep(1000);

                string serviceText = service.Text;
                Console.WriteLine("Service provided: " + serviceText);
                test.Log(Status.Pass, $"DevOps Service found with text: {serviceText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC18_Failure");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(19)]
        public void TC19_CheckSliderText()
        {
            test = extent.CreateTest("TC19_CheckSliderText", "Verify the text changes when slider arrow is clicked");

            try
            {
                var sliderArrow = driver.FindElement(By.XPath("//sr7-arrow[contains(@class, 'sr7-leftarrow')]"));
                sliderArrow.Click();
                test.Log(Status.Info, "Clicked on left arrow of the slider.");
                Thread.Sleep(1000);

                bool isText1 = driver.FindElements(By.XPath("//sr7-txt[text()='Empower Your Workforce with AI-Driven Digital Workers']")).Count > 0;
                bool isText2 = driver.FindElements(By.XPath("//sr7-txt[text()='Accelerate Digital Transformation with AI & Automation']")).Count > 0;

                if (isText1)
                {
                    test.Log(Status.Pass, "Text 1 is present: Empower Your Workforce with AI-Driven Digital Workers");
                    Console.WriteLine("Text 1 is present");
                }
                else if (isText2)
                {
                    test.Log(Status.Pass, "Text 2 is present: Accelerate Digital Transformation with AI & Automation");
                    Console.WriteLine("Text 2 is present");
                }
                else
                {
                    string screenshotPath = CaptureScreenshot("TC19_NoTextFound");
                    test.Log(Status.Fail, "Neither of the expected texts is present.")
                        .AddScreenCaptureFromPath(screenshotPath);
                    Assert.Fail("Expected slider text not found.");
                }
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC19_Exception");
                test.Log(Status.Fail, $"Test failed due to exception: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(20)]
        public void TC20_CheckSecondSliderText()
        {
            test = extent.CreateTest("TC20_CheckSecondSliderText", "Verify second slider text content after clicking the slider arrow");

            try
            {
                var sliderArrow = driver.FindElement(By.XPath("//sr7-arrow[contains(@class, 'sr7-leftarrow')]"));
                sliderArrow.Click();
                test.Log(Status.Info, "Clicked left arrow on slider.");
                Thread.Sleep(1000);

                bool isText1 = driver.FindElements(By.XPath("//sr7-txt[text()='Empower Your Workforce with AI-Driven Digital Workers']")).Count > 0;
                bool isText2 = driver.FindElements(By.XPath("//sr7-txt[text()='Accelerate Digital Transformation with AI & Automation']")).Count > 0;

                if (isText1)
                {
                    test.Log(Status.Pass, "Text 3 is present: Empower Your Workforce with AI-Driven Digital Workers");
                    Console.WriteLine("Text 3 is present");
                }
                else if (isText2)
                {
                    test.Log(Status.Pass, "Text 4 is present: Accelerate Digital Transformation with AI & Automation");
                    Console.WriteLine("Text 4 is present");
                }
                else
                {
                    string screenshotPath = CaptureScreenshot("TC20_NoTextFound");
                    test.Log(Status.Fail, "Neither of the expected texts is present on the slider.")
                        .AddScreenCaptureFromPath(screenshotPath);
                    Assert.Fail("Expected slider text not found.");
                }
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC20_Exception");
                test.Log(Status.Fail, $"Test failed due to exception: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        [Test, Order(21)]
        public void TC21_VerifyPlatformHeading()
        {
            test = extent.CreateTest("TC21_VerifyPlatformHeading", "Verify that the 'Platform' heading is visible on the page.");

            try
            {
                var heading = driver.FindElement(By.XPath("(//div[@class='pxl-heading--inner']/h3)[5]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", heading);
                string headingText = heading.Text;

                Console.WriteLine("Heading Text: " + headingText);
                test.Log(Status.Pass, $"Platform heading found: {headingText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC21_VerifyPlatformHeading");
                test.Log(Status.Fail, $"Test failed due to exception: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(22)]
        public void TC22_VerifyAssessmentAndIntegration()
        {
            test = extent.CreateTest("TC22_VerifyAssessmentAndIntegration", "Verify the presence of 'Assessment and Integration' section and its details.");

            try
            {
                var title = driver.FindElement(By.XPath("//div[@class='pxl-list']//div[normalize-space(text())='Assessment and Integration']"));
                string titleText = title.Text;
                Console.WriteLine("Title: " + titleText);
                test.Log(Status.Pass, $"Section title found: {titleText}");

                var details = driver.FindElement(By.XPath("(//div[@class='pxl-item-desc1'])[1]"));
                string detailsText = details.Text;
                Console.WriteLine("Values Details: " + detailsText);
                test.Log(Status.Pass, $"Details found: {detailsText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC22_VerifyAssessmentAndIntegration");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        [Test, Order(23)]
        public void TC23_VerifyCustomizationAndDeployment()
        {
            test = extent.CreateTest("TC23_VerifyCustomizationAndDeployment", "Verify the 'Customization and Deployment' section and its details.");

            try
            {
                var title = driver.FindElement(By.XPath("//div[@class='pxl-item--text1' and normalize-space(.)='Customization and Deployment']"));
                string titleText = title.Text;
                Console.WriteLine("Title: " + titleText);
                test.Log(Status.Pass, $"Section title found: {titleText}");

                var details = driver.FindElement(By.XPath("//div[@class='pxl-item-desc1' and contains(., 'Address specific industry challenges')]"));
                string detailsText = details.Text;
                Console.WriteLine("Values Details: " + detailsText);
                test.Log(Status.Pass, $"Details found: {detailsText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC23_VerifyCustomizationAndDeployment");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(24)]
        public void TC24_VerifyAutomationAndOptimization()
        {
            test = extent.CreateTest("TC24_VerifyAutomationAndOptimization", "Verify the 'Automation and Optimization' section and its details.");

            try
            {
                var title = driver.FindElement(By.XPath("//div[@class='pxl-item--text1' and contains(normalize-space(.), 'Automation and Optimization')]"));
                string titleText = title.Text;
                Console.WriteLine("Title: " + titleText);
                test.Log(Status.Pass, $"Section title found: {titleText}");

                var details = driver.FindElement(By.XPath("//div[@class='pxl-item-desc1' and contains(., 'Utilize insights from KamerAI')]"));
                string detailsText = details.Text;
                Console.WriteLine("Values Details: " + detailsText);
                test.Log(Status.Pass, $"Details found: {detailsText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC24_VerifyAutomationAndOptimization");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(25)]
        public void TC25_ClickGetStartedAndHandleWindows()
        {
            test = extent.CreateTest("TC25_ClickGetStartedAndHandleWindows", "Click on 'Get Started' and handle new window.");

            try
            {
                originalWindow = driver.CurrentWindowHandle;

                var getStartedBtn = driver.FindElement(By.XPath("//span[normalize-space(text())='Get Started']"));
                getStartedBtn.Click();
                test.Log(Status.Info, "Clicked on 'Get Started' button.");
                Thread.Sleep(2000);

                var windows = driver.WindowHandles;
                bool newWindowHandled = false;

                foreach (var win in windows)
                {
                    if (win != originalWindow)
                    {
                        driver.SwitchTo().Window(win);
                        Thread.Sleep(1000);
                        driver.Close();
                        test.Log(Status.Pass, "Switched to and closed new window.");
                        newWindowHandled = true;
                        break;
                    }
                }

                driver.SwitchTo().Window(originalWindow);

                if (!newWindowHandled)
                {
                    test.Log(Status.Warning, "No new window appeared after clicking 'Get Started'.");
                }
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC25_ClickGetStartedAndHandleWindows");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        [Test, Order(26)]
        public void TC26_VerifyAchievementText()
        {
            test = extent.CreateTest("TC26_VerifyAchievementText", "Verify the 'Achievement' section text is visible and correct.");

            try
            {
                driver.SwitchTo().Window(originalWindow);
                test.Log(Status.Info, "Switched to original browser window.");

                var achievement = driver.FindElement(By.XPath("(//h3[@class='pxl-item--title style-default highlight-default '])[5]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", achievement);
                Thread.Sleep(2000);

                string achievementText = achievement.Text;
                Console.WriteLine("Achievement Text: " + achievementText);
                test.Log(Status.Pass, $"Achievement text found: {achievementText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC26_VerifyAchievementText");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(27)]
        public void TC27_VerifyBrandMessage()
        {
            test = extent.CreateTest("TC27_VerifyBrandMessage", "Verify the brand message 'Reimagine, Reengineered, Recode' is visible.");

            try
            {
                var brandMsg = driver.FindElement(By.XPath("//span[normalize-space(text())='Reimagine, Reengineered, Recode']"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", brandMsg);
                string message = brandMsg.Text;
                Console.WriteLine("Brand Message: " + message);

                test.Log(Status.Pass, $"Brand message displayed: {message}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC27_VerifyBrandMessage");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(28)]
        public void TC28_ClickReadMoreAndVerifyCompany()
        {
            test = extent.CreateTest("TC28_ClickReadMoreAndVerifyCompany", "Click 'Read More' and verify 'Company' text is displayed.");

            try
            {
                var readMore = driver.FindElement(By.XPath("//span[normalize-space(text())='Read More']"));
                test.Log(Status.Info, "Located 'Read More' button.");
                readMore.Click();
                test.Log(Status.Info, "'Read More' button clicked.");
                Thread.Sleep(1000);

                var companyText = driver.FindElement(By.XPath("(//span[normalize-space(text())='Company'])[4]"));
                string text = companyText.Text;
                Console.WriteLine("Company Text: " + text);

                test.Log(Status.Pass, $"Company text displayed: {text}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC28_ClickReadMoreAndVerifyCompany");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(29)]
        public void TC29_NavigateBackToHome()
        {
            test = extent.CreateTest("TC29_NavigateBackToHome", "Click 'Home' to return to the homepage.");

            try
            {
                var home = driver.FindElement(By.XPath("(//span[normalize-space(text())='Home'])[1]"));
                test.Log(Status.Info, "Located 'Home' button.");

                home.Click();
                test.Log(Status.Pass, "'Home' button clicked successfully.");
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC29_NavigateBackToHome");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(30)]
        public void TC30_VerifyCareersSection()
        {
            test = extent.CreateTest("TC30_VerifyCareersSection", "Verify 'Careers at Recode' section is displayed.");

            try
            {
                var careers = driver.FindElement(By.XPath("//h3[contains(@class, 'pxl-item--title') and contains(., 'Careers at Recode')]"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", careers);
                string text = careers.Text;
                Console.WriteLine("Careers Text: " + text);

                test.Log(Status.Pass, $"Careers section found: {text}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC30_VerifyCareersSection");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(31)]
        public void TC31_ClickViewMoreAndVerifyText()
        {
            test = extent.CreateTest("TC31_ClickViewMoreAndVerifyText", "Click 'View More' and verify the recruitment text.");

            try
            {
                var viewMore = driver.FindElement(By.XPath("//span[normalize-space(text())='View More']"));
                string viewMoreText = viewMore.Text;
                Console.WriteLine("View More Text: " + viewMoreText);
                test.Log(Status.Info, $"Located 'View More' button with text: {viewMoreText}");

                viewMore.Click();
                test.Log(Status.Pass, "'View More' button clicked successfully.");
                Thread.Sleep(1000);

                var recruit = driver.FindElement(By.XPath("//h2[contains(text(), \"We're more than just a workplace\")]"));
                string recruitText = recruit.Text;
                Console.WriteLine("Recruit Text: " + recruitText);
                test.Log(Status.Pass, $"Recruitment text verified: {recruitText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC31_ClickViewMoreAndVerifyText");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(32)]
        public void TC32_NavigateBackToHomeAndHandleWindows()
        {
            test = extent.CreateTest("TC32_NavigateBackToHomeAndHandleWindows", "Navigate back to Home and handle multiple browser windows.");

            try
            {
                string currentWindow = driver.CurrentWindowHandle;
                test.Log(Status.Info, $"Current window handle: {currentWindow}");

                var homeLink = driver.FindElement(By.XPath("//a[text()='Home']"));
                homeLink.Click();
                test.Log(Status.Info, "'Home' link clicked.");

                Thread.Sleep(2000);

                var windows = driver.WindowHandles;
                Console.WriteLine("Total windows: " + windows.Count);
                test.Log(Status.Info, $"Total windows after navigation: {windows.Count}");

                if (windows.Count > 1)
                {
                    driver.SwitchTo().Window(windows[1]);
                    test.Log(Status.Pass, "Switched to the new window successfully.");
                }
                else
                {
                    test.Log(Status.Pass, "No additional windows to switch to.");
                }
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC32_NavigateBackToHomeAndHandleWindows");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(33)]
        public void TC33_VerifyLifeAtRecode()
        {
            test = extent.CreateTest("TC33_VerifyLifeAtRecode", "Verify the 'Life @ Recode' section is visible and contains the expected text.");

            try
            {
                var life = driver.FindElement(By.XPath("//a[text()='Life @ Recode']"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", life);
                string lifeText = life.Text;

                Console.WriteLine("Careers: " + lifeText);
                test.Log(Status.Pass, $"'Life @ Recode' section found with text: {lifeText}");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC33_VerifyLifeAtRecode");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }


        [Test, Order(34)]
        public void TC34_VerifyAwardsSection()
        {
            test = extent.CreateTest("TC34_VerifyAwardsSection", "Verify the 'Awards' section heading and image are displayed correctly.");

            try
            {
                var heading = driver.FindElement(By.XPath("(//div[@data-id='570067b']//div[contains(@class, 'pxl-heading')])[2]"));
                string headingText = heading.Text;
                Console.WriteLine("Awards Heading: " + headingText);
                test.Log(Status.Pass, $"Awards heading found: {headingText}");

                var img = driver.FindElement(By.XPath("//img[contains(@src, 'Awards-1.png')]"));
                bool isVisible = img.Displayed;
                Console.WriteLine("Awards image visible: " + isVisible);

                if (isVisible)
                {
                    test.Log(Status.Pass, "Awards image is displayed correctly.");
                }
                else
                {
                    test.Log(Status.Fail, "Awards image is not visible.");
                }
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("TC34_VerifyAwardsSection");
                test.Log(Status.Fail, $"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

    }
}
