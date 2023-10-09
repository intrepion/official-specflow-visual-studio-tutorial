using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CalculatorSelenium.Specs.Drivers
{
    /// <summary>
    /// Manages a browser instance using Selenium
    /// </summary>
    public class BrowserDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public BrowserDriver()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// <summary>
        /// The Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => _currentWebDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateWebDriver()
        {
            //We use the Firefox browser
            var firefoxDriverService = FirefoxDriverService.CreateDefaultService();

            var firefoxOptions = new FirefoxOptions();

            var firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxOptions);

            return firefoxDriver;
        }

        /// <summary>
        /// Disposes the Selenium web driver (closing the browser) after the Scenario completed
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
