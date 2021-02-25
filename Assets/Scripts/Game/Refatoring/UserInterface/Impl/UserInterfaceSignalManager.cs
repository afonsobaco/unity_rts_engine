﻿using System.Collections.Generic;
using System.Linq;
using RTSEngine.Signal;
using UnityEngine;

namespace RTSEngine.Refactoring
{
    public class UserInterfaceSignalManager
    {
        private UserInterfaceManager _userInterfaceManager;
        private UserInterface _userInterface;

        public UserInterfaceSignalManager(UserInterfaceManager userInterfaceManager, UserInterface userInterface)
        {
            this._userInterfaceManager = userInterfaceManager;
            this._userInterface = userInterface;
        }

        public void OnSelectionUpdate(SelectionUpdateSignal signal)
        {
            _userInterface.DoSelectionUpdate(signal.Selection);
        }

        public void OnPartyUpdate(PartyUpdateSignal signal)
        {
            _userInterface.DoPartyUpdate(signal.Parties);
        }

        public void OnAlternateSubGroup(AlternateSubGroupSignal signal)
        {
            _userInterface.AlternateSubGroup(signal.Previous);
        }


        public void OnMiniatureClicked(MiniatureClickedSignal signal)
        {
            _userInterfaceManager.DoMiniatureClicked(_userInterface.Selection, signal.Selected, signal.ToRemove, signal.AsSubGroup);
        }
        public void OnPortraitClicked(PortraitClickedSignal signal)
        {
            _userInterfaceManager.DoPortraitClicked(signal.Selection);
        }
        public void OnBannerClicked(BannerClickedSignal signal)
        {
            Core.ISelectable[] value;
            _userInterface.Parties.TryGetValue(signal.PartyId, out value);
            _userInterfaceManager.DoBannerClicked(_userInterface.Selection, value, signal.ToRemove);
        }
        public void OnMapClicked(MapClickedSignal signal)
        {
            _userInterfaceManager.DoMapClicked(signal.Selection);
        }
        public void OnActionClicked(ActionClickedSignal signal)
        {
            _userInterfaceManager.DoActionClicked(signal.Selection);
        }

    }
}
