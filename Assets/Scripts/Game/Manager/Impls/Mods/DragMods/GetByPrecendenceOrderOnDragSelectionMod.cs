﻿
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using RTSEngine.Core;

namespace RTSEngine.Manager.Impls.Deprecated
{

    public class GetByPrecendenceOrderOnDragSelectionMod<T, E, O> : AbstractDragSelectionMod<T, E, O>
    {

        // protected override List<SelectableObject> Execute(SelectionArgs args)
        // {

        //     List<SelectableObject> filteredList = args.NewList.FindAll(a => args.Settings.PrimaryTypes.Contains(a.type));
        //     if (filteredList.Count == 0)
        //     {
        //         if (args.IsPreSelection)
        //         {
        //             return new List<SelectableObject>();
        //         }
        //         filteredList = GetSecondarySelection(args);
        //     }
        //     if (args.OldList.Count > 0)
        //     {
        //         filteredList = args.OldList.Union(filteredList).ToList();
        //     }
        //     return filteredList;
        // }

        // private List<SelectableObject> GetSecondarySelection(SelectionArgs args)
        // {
        //     foreach (var secondaryType in args.Settings.SecondaryOrderedTypes)
        //     {
        //         List<SelectableObject> list = args.NewList.FindAll(a => a.type == secondaryType);
        //         if (list.Count > 0)
        //         {
        //             return new List<SelectableObject>() { list[Random.Range(0, list.Count)] };
        //         }
        //     }
        //     return new List<SelectableObject>();
        // }
        public override SelectionArgsXP<T, E, O> Apply(SelectionArgsXP<T, E, O> args)
        {
            throw new System.NotImplementedException();
        }
    }
}