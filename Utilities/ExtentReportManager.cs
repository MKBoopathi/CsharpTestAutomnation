using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

namespace RecodeTests.Utilities
{
    public static class ExtentReportManager
    {
        private static ExtentReports _extent;
        private static ExtentTest _test;
        private static ExtentHtmlReporter _htmlReporter;

        public static void InitReport()
        {
            string reportPath = $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html";
            _htmlReporter = new ExtentHtmlReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_htmlReporter);
        }

        public static void FlushReport()
        {
            _extent.Flush();
        }

        public static void CreateTest(string testName)
        {
            _test = _extent.CreateTest(testName);
        }

        public static ExtentTest GetTest()
        {
            return _test;
        }

        public static void LogInfo(string message)
        {
            _test?.Info(message);
        }

        public static void LogPass(string message)
        {
            _test?.Pass(message);
        }

        public static void LogFail(string message)
        {
            _test?.Fail(message);
        }
    }
}
