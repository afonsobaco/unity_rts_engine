using System.Linq;
using UnityEngine;
using NUnit.Framework;
using RTSEngine.Core;
using NSubstitute;
using RTSEngine.Manager;
using System.Collections.Generic;
using Tests.Utils;

namespace Tests
{
    [TestFixture]
    public class LimitModifierTest
    {
        private LimitSelectionModifier modifier;
        private ISelectionManager<ISelectableObject, SelectionTypeEnum> selectionManager;

        [SetUp]
        public void SetUp()
        {
            selectionManager = Substitute.For<ISelectionManager<ISelectableObject, SelectionTypeEnum>>();
            modifier = Substitute.ForPartsOf<LimitSelectionModifier>(new object[] { selectionManager });

        }

        [Test]
        public void SelectionLimitModifierTestSimplePasses()
        {
            SelectionArgsXP args = new SelectionArgsXP(new HashSet<ISelectableObject>(), new HashSet<ISelectableObject>(), new HashSet<ISelectableObject>());
            var result = modifier.Apply(args);
            Assert.AreEqual(args, result);
        }


        [TestCaseSource(nameof(Scenarios))]
        public void ShouldLimitSelectionToPassedValue(SelectionStruct selectionStruct, ModifiersStruct modifiersStruct, ResultStruct resultStruct, int limit)
        {
            HashSet<ISelectableObject> mainList = TestUtils.GetSomeObjects(selectionStruct.mainListAmount);
            HashSet<ISelectableObject> oldSelection = TestUtils.GetListByIndex(selectionStruct.oldSelection, mainList);
            HashSet<ISelectableObject> newSelection = TestUtils.GetListByIndex(selectionStruct.newSelection, mainList);

            SelectionArgsXP args = new SelectionArgsXP(oldSelection, newSelection, mainList);
            ISelectionSettings settings = Substitute.For<ISelectionSettings>();
            selectionManager.GetSettings().Returns(settings);
            settings.Limit.Returns(limit);

            args = modifier.Apply(args);
            HashSet<ISelectableObject> expected = TestUtils.GetListByIndex(resultStruct.expected, mainList);
            CollectionAssert.AreEquivalent(expected, args.ToBeAdded);
        }



        private static IEnumerable<TestCaseData> Scenarios
        {
            get
            {
                foreach (var item in TestUtils.GetDefaultCases())
                {
                    const int limit = 3;
                    yield return new TestCaseData(item.selection, item.modifiers, new ResultStruct()
                    {
                        expected = item.selection.newSelection.Take(limit).ToArray(),
                    }, limit).SetName(item.name);
                }
            }
        }

    }
}
