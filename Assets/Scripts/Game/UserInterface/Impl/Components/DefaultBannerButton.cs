﻿using System;
using UnityEngine;
using Zenject;

namespace RTSEngine.RTSUserInterface
{
    public class DefaultBannerButton : DefaultClickable
    {
        public override void DoClick()
        {
            Debug.Log("Unimplemented: Banner");
        }

        public override void DoPress()
        {
            Debug.Log("Unimplemented: Banner Press");
        }

        public class Factory : PlaceholderFactory<DefaultBannerButton>
        {
        }

    }
}
