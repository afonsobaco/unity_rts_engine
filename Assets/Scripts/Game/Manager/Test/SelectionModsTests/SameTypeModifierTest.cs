﻿using UnityEngine;
using NUnit.Framework;
using RTSEngine.Core;
using RTSEngine.Manager;
using RTSEngine.Manager.SelectionMods.Impls;
using System.Collections.Generic;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class SameTypeModifierTest
    {
        private SameTypeModifier.SelectionModifier modifier;

        [SetUp]
        public void SetUp()
        {
            Modifier = Substitute.ForPartsOf<SameTypeModifier.SelectionModifier>();
        }

        [Test]
        public void SameTypeModifierTestSimplePasses()
        {
            SelectionArguments arguments = new SelectionArguments(SelectionTypeEnum.ALL, false, new List<ISelectable>(), new List<ISelectable>(), new List<ISelectable>());
            SelectionModifierArguments modifierArguments = new SelectionModifierArguments(false, false, Vector2.zero, Vector2.zero);
            SelectionArgsXP args = new SelectionArgsXP(arguments, modifierArguments);

            var result = Modifier.Apply(args);
            Assert.AreEqual(args, result);
        }


        [TestCaseSource(nameof(Scenarios))]
        public void ShouldApplyModifier(SelectionTypeEnum selectionType, int mainListCount, bool isSameType, int[] sameTypeSelection, int[] oldSelection, int[] newSelection, int[] expectedToBeAdded, int[] expectedToBeRemoved)
        {

            List<ISelectable> mainList = ModifierTestUtils.GetSomeObjects(mainListCount);

            SelectionArguments arguments = new SelectionArguments(selectionType, false, ModifierTestUtils.GetListByIndex(oldSelection, mainList), ModifierTestUtils.GetListByIndex(newSelection, mainList), mainList);
            SelectionModifierArguments modifierArguments = new SelectionModifierArguments(isSameType, false, Vector2.zero, new Vector2(800, 600));
            SelectionArgsXP args = new SelectionArgsXP(arguments, modifierArguments);

            List<ISelectable> sameTypeList = ModifierTestUtils.GetListByIndex(sameTypeSelection, mainList);

            Modifier.When(x => x.GetAllFromSameTypeOnScreen(default)).DoNotCallBase();
            Modifier.GetAllFromSameTypeOnScreen(Arg.Any<SelectionArgsXP>()).Returns(sameTypeList);

            args = Modifier.Apply(args);

            List<ISelectable> expectedToBeAddedResult = ModifierTestUtils.GetListByIndex(expectedToBeAdded, mainList);
            CollectionAssert.AreEquivalent(expectedToBeAddedResult, args.Result.ToBeAdded);

            List<ISelectable> expectedToBeRemovedResult = ModifierTestUtils.GetListByIndex(expectedToBeRemoved, mainList);
            CollectionAssert.AreEquivalent(expectedToBeRemovedResult, args.Result.ToBeRemoved);
        }

        public static IEnumerable<TestCaseData> Scenarios
        {
            get
            {
                //Click
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { }, new int[] { 0 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { }).SetName("Click - SameType, Empty Old");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0 }, new int[] { 0 }, new int[] { }, new int[] { 0, 1, 2, 3, 4 }).SetName("Click - SameType, Single element in Old, Clicked in Old");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0 }, new int[] { 1 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0 }).SetName("Click - SameType, Single element in Old, Clicked NOT in Old, Old is of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { 7 }, new int[] { 0 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 7 }).SetName("Click - SameType, Single element in Old, Clicked NOT in Old, Old is NOT of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 7 }, new int[] { 0 }, new int[] { 7 }, new int[] { 0, 1, 2, 3, 4, 7 }).SetName("Click - SameType, Multiple element in Old, Clicked in Old, Old has only clicked of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 1, 7 }, new int[] { 0 }, new int[] { 7 }, new int[] { 0, 1, 2, 3, 4, 7 }).SetName("Click - SameType, Multiple element in Old, Clicked in Old, Old has more of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 1, 7 }, new int[] { 2 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 1, 7 }).SetName("Click - SameType, Multiple element in Old, Clicked NOT in Old, Old contains some of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, true, new int[] { 0, 1, 2, 3, 4 }, new int[] { 5, 6, 7 }, new int[] { 0 }, new int[] { 0, 1, 2, 3, 4 }, new int[] { 5, 6, 7 }).SetName("Click - SameType, Multiple element in Old, Clicked NOT in Old, Old does NOT contains any of same type");

                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { }, new int[] { 0 }, new int[] { 0 }, new int[] { }).SetName("Click - NOT SameType, Empty Old");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 }).SetName("Click - NOT SameType, Single element in Old, Clicked in Old");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { 0 }, new int[] { 1 }, new int[] { 1 }, new int[] { 0 }).SetName("Click - NOT SameType, Single element in Old, Clicked NOT in Old, Old is of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { 7 }, new int[] { 0 }, new int[] { 0 }, new int[] { 7 }).SetName("Click - NOT SameType, Single element in Old, Clicked NOT in Old, Old is NOT of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { 0, 7 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0, 7 }).SetName("Click - NOT SameType, Multiple element in Old, Clicked in Old, Old has only clicked of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { 0, 1, 7 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0, 1, 7 }).SetName("Click - NOT SameType, Multiple element in Old, Clicked in Old, Old has more of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { 0, 1, 7 }, new int[] { 1 }, new int[] { 1 }, new int[] { 0, 1, 7 }).SetName("Click - NOT SameType, Multiple element in Old, Clicked NOT in Old, Old contains some of same type");
                yield return new TestCaseData(SelectionTypeEnum.CLICK, 10, false, new int[] { }, new int[] { 5, 6, 7 }, new int[] { 0 }, new int[] { 0 }, new int[] { 5, 6, 7 }).SetName("Click - NOT SameType, Multiple element in Old, Clicked NOT in Old, Old does NOT contains any of same type");
            }
        }

        public SameTypeModifier.SelectionModifier Modifier { get => modifier; set => modifier = value; }
    }
}
