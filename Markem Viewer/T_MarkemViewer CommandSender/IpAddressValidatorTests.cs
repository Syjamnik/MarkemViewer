using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MarkemViewer_CommandSender;

namespace T_MarkemViewer_CommandSender
{
    [TestFixture]
    class IpAddressValidatorTests
    {

        [TestCase("172.20.25.11")]
        [TestCase("255.255.255.254")]
        [TestCase("0.0.0.1")]
        public void ValidateIpAddress_WhenCorrectAddressIsGiven_returnsTrue(string ipAddress)
        {
            Assert.IsTrue(ipAddress.ValidateIpAddress());
        }

        [TestCase("255.255.255.255")]
        [TestCase("0.0.0.0")]
        public void ValidateIpAddress_WhenAddressIsOutOfRange_returnsFalse(string ipAddress)
        {
            Assert.IsFalse(ipAddress.ValidateIpAddress());
        }
        [TestCase("255.255.255.254.11")]
        [TestCase("255.255.11")]
        public void ValidateIpAddress_WhenAddressLengthIsInvalid_returnsFalse(string ipAddress)
        {
            Assert.IsFalse(ipAddress.ValidateIpAddress());
        }
        [TestCase("255.255.12.gf")]
        public void ValidateIpAddress_WhenAddressContainsNonNumbers_returnsFalse(string ipAddress)
        {
            Assert.IsFalse(ipAddress.ValidateIpAddress());
        }
        [TestCase("255. 255.1 2.13")]
        public void ValidateIpAddress_WhenAddressContainsNumbersWithWhiteSpaces_returnsFalse(string ipAddress)
        {
            Assert.IsFalse(ipAddress.ValidateIpAddress());
        }
        [TestCase("255.255..13")]
        public void ValidateIpAddress_WhenPartOfAddressIsEmpty_returnsFalse(string ipAddress)
        {
            Assert.IsFalse(ipAddress.ValidateIpAddress());
        }

    }
}
