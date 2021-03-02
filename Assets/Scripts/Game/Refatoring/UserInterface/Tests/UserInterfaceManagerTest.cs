﻿using System;
using System.Linq;
using NUnit.Framework;
using RTSEngine.Core;
using RTSEngine.Refactoring;
using RTSEngine.Signal;
using NSubstitute;
using Tests.Utils;
using RTSEngine.Utils;
using RTSEngine.Commons;

namespace Tests
{
    [TestFixture]
    public class UserInterfaceManagerTest
    {
        private UserInterfaceManager _userInterfaceManager;
        private GameSignalBus _signalBus;

        [SetUp]
        public void SetUp()
        {
            _signalBus = Substitute.ForPartsOf<GameSignalBus>(new object[] { default });
            _userInterfaceManager = Substitute.ForPartsOf<UserInterfaceManager>(new object[] { _signalBus, default });
            _signalBus.WhenForAnyArgs(x => x.Fire(default)).DoNotCallBase();
        }

        [Test]
        public void UserInterfaceManagerTestSimplePasses()
        {
            Assert.NotNull(_userInterfaceManager);
        }

        [Test]
        public void ShouldDoNothingWhenDoMiniatureClickedWithNull()
        {
            _userInterfaceManager.DoMiniatureClicked(null);
            _signalBus.DidNotReceiveWithAnyArgs().Fire(default);
        }

        [Test]
        public void ShouldReturnClickedWhenDoMiniatureClicked()
        {
            ISelectable clicked = TestUtils.GetSomeObjects(1).First();
            _userInterfaceManager.DoMiniatureClicked(clicked);
            _signalBus.Received().Fire(Arg.Any<IndividualSelectionSignal>());
        }

        [Test]
        public void ShouldDoNothingWhenDoPortraitClickedWithNull()
        {
            _userInterfaceManager.DoPortraitClicked(null);
            _signalBus.DidNotReceiveWithAnyArgs().Fire(default);
        }

        [Test]
        public void ShouldDoPortraitClicked()
        {
            ISelectable clicked = TestUtils.GetSomeObjects(1).First();
            _userInterfaceManager.DoPortraitClicked(clicked);
            _signalBus.Received().Fire(Arg.Any<CameraGoToPositionSignal>());
        }

        [Test]
        public void ShouldDoNothingWhenDoBannerClickedWithNull()
        {
            _userInterfaceManager.DoBannerClicked(default);
            _signalBus.DidNotReceiveWithAnyArgs().Fire(default);
        }

        [Test]
        public void ShouldDoBannerClicked()
        {
            string partyId = "partyIdAsString";
            _userInterfaceManager.DoBannerClicked(partyId);
            _signalBus.Received().Fire(Arg.Is<PartySelectionSignal>(x => x.CreateNew == false && x.PartyId.Equals(partyId)));
        }

    }
}
