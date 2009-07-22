﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WCFChannelManager;

namespace WCFChannelManagerTests
{
    [TestFixture]
    public class WcfChannelProxyTypeBuilderTests
    {
        [Test, ExpectedException(typeof(ArgumentException))]
        public void Constructor_WhenCalledWithAnIncorrectType_ThrowsException()
        {
            WcfChannelProxyTypeBuilder builder = new WcfChannelProxyTypeBuilder(typeof(TimeZone));
        }

        [Test]
        public void IsSubclassOfRawGeneric_WhenCalledWithClosedGenericSubclass_FindsGenericType()
        { 
            Assert.IsTrue(WcfChannelProxyTypeBuilder.IsSubclassOfRawGeneric(typeof(ProxyChannel<>),typeof(StubProxyChannel)));
            Assert.IsFalse(WcfChannelProxyTypeBuilder.IsSubclassOfRawGeneric(typeof(ProxyChannel<>), typeof(TimeZone)));
        }

        [Test]
        public void FindChannelType_WhenCalledWithSubclass_FindsChannelType()
        { 
            Type t = WcfChannelProxyTypeBuilder.FindChannelType(typeof(StubProxyChannel));
            Assert.IsNotNull(t);
            Assert.AreEqual(t, typeof(IService));
        }

        [Test]
        public void BuildProxyType_WhenCalled_ImplementsInterfaceAndSubclassesProxyChannel()
        {
            WcfChannelProxyTypeBuilder builder = new WcfChannelProxyTypeBuilder(typeof(ProxyChannel<IService>));
            Type t = builder.BuildProxyType();
            Assert.IsNotNull(t);
            Assert.IsTrue(t.IsSubclassOf(typeof(ProxyChannel<IService>)));
            Assert.IsNotNull(t.GetInterface("IService", true));
        }
    }
}
