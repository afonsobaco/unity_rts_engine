﻿using System;

namespace RTSEngine.Manager.Impls.Deprecated
{
    public class ShuffleSelectedSameTypeOnClickSelectionMod<T, E, O> : AbstractClickSelectionMod<T, E, O>
    {
        // protected override List<SelectableObject> Execute(SelectionArgs args)
        // {
        //     if ((!args.IsSameType && !args.IsDoubleClick) || !args.NewList.Contains(args.Clicked) )
        //     {
        //         return args.NewList;
        //     }
        //     return ShuffleOnlyClickedSameType(args);
        // }

        // private List<SelectableObject> ShuffleOnlyClickedSameType(SelectionArgs args)
        // {
        //     List<SelectableObject> listOfSameType = args.NewList.FindAll(a => SameTypeUtil.isSameType(args.Clicked, a));
        //     List<SelectableObject> listWhitout = args.NewList.FindAll(a => !SameTypeUtil.isSameType(args.Clicked, a));

        //     listOfSameType = SelectionUtil.Shuffle(listOfSameType);

        //     return listWhitout.Union(listOfSameType).ToList();
        // }
        public override SelectionArgsXP<T, E, O> Apply(SelectionArgsXP<T, E, O> args)
        {
            throw new NotImplementedException();
        }
    }
}