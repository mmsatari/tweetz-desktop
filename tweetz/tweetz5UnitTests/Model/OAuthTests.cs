﻿// Copyright (c) 2012 Blue Onion Software - All rights reserved

using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tweetz5.Model;

namespace tweetz5UnitTests.Model
{
    [TestClass]
    public class OAuthTests
    {
        [TestMethod]
        public void UrlEncodeEncodeTests()
        {
            Assert.AreEqual(OAuth.UrlEncode("%"), "%25");
            const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            OAuth.UrlEncode(unreservedChars).Should().Be(unreservedChars);
        }

        [TestMethod]
        public void UrlEncodeArabic()
        {
            OAuth.UrlEncode("الخيال اهلا وسهلا بكم في عالم")
                .Should()
                .Be("%D8%A7%D9%84%D8%AE%D9%8A%D8%A7%D9%84%20%D8%A7%D9%87%D9%84%D8%A7%20%D9%88%D8%B3%D9%87%D9%84%D8%A7%20%D8%A8%D9%83%D9%85%20%D9%81%D9%8A%20%D8%B9%D8%A7%D9%84%D9%85");
        }

        [TestMethod]
        public void NounceReturnsDifferntValuesOnEachCall()
        {
            OAuth.Nonce().Should().NotBe(OAuth.Nonce());
        }

        [TestMethod]
        public void TimeStampReturnsValid64IntString()
        {
            var timeStamp = OAuth.TimeStamp();
            var int64 = Int64.Parse(timeStamp);
            int64.Should().BeGreaterThan(1352852131);
        }

        [TestMethod]
        public void AuthorizationHeaderIsCorrectFormat()
        {
            const string header = "OAuth oauth_consumer_key=\"ZScn2AEIQrfC48Zlw\"," +
                                  "oauth_nonce=\"123\"," +
                                  "oauth_signature=\"signature\"," +
                                  "oauth_signature_method=\"HMAC-SHA1\"," +
                                  "oauth_timestamp=\"345\"," +
                                  "oauth_token=\"s8I\"," +
                                  "oauth_version=\"1.0\"";
            OAuth.AuthorizationHeader("123", "345", "s8I", "signature").Should().Be(header);
        }

        [TestMethod]
        public void SignatureMethodTest()
        {
            var signature = OAuth.Signature("GET", "http://twitter.com", "123", "456", "s8I", "zEgShI", new[] {new[] {"entities", "true"}});
            signature.Should().Be("u+LenucADw1l+2Kx6W0NedXLrpc=");
        }
    }
}