// ---------------------------------------------------------------------------------------------------
// <copyright file="AppSettingsTests.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-14</date>
// <summary>
//     The AppSettingsTests class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.Tests.Helper
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Youffer.Common.Helper;

    /// <summary>
    /// The AppSettings tests cases
    /// </summary>
    [TestClass]
    public class AppSettingsTests
    {
        /// <summary>
        /// The default vale
        /// </summary>
        private const string DefaultVale = "defaultVale";

        /// <summary>
        /// Reads the existing settings.
        /// </summary>
        [TestMethod]
        public void ReadExistingSettings()
        {
            var value = AppSettings.Get<string>("testStringValue");

            Assert.IsNotNull(value);
        }

        /// <summary>
        /// Tries the read existing settings with default value.
        /// </summary>
        [TestMethod]
        public void TryReadExistingSettingsWithDefaultValue()
        {
            var value = AppSettings.Get("testStringValue", DefaultVale);

            Assert.IsNotNull(value);
            Assert.AreNotEqual(value, DefaultVale);
        }

        /// <summary>
        /// Tries to read non existing settings.
        /// </summary>
        [TestMethod]
        public void TryToReadNonExistingSettings()
        {
            var value = AppSettings.Get<string>("testStringValueNotExists");

            Assert.IsNull(value);
        }

        /// <summary>
        /// Tries to read non existing settings with default value.
        /// </summary>
        [TestMethod]
        public void TryToReadNonExistingSettingsWithDefaultValue()
        {
            var value = AppSettings.Get("testStringValueNotExists", DefaultVale);

            Assert.IsNotNull(value);
            Assert.AreEqual(DefaultVale, value);
        }
    }
}
