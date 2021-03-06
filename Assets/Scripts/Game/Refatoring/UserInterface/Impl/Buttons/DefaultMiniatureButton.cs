﻿using UnityEngine;
using Zenject;

namespace RTSEngine.Refactoring
{
    public class DefaultMiniatureButton : DefaultClickableButton
    {
        public override void DoClick()
        {
            Debug.Log("Unimplemented: Miniature");
        }

        public override void DoPress()
        {
            Debug.Log("Unimplemented: Miniature Press");
        }
        public class Factory : PlaceholderFactory<DefaultMiniatureButton>
        {
        }
    }
}
