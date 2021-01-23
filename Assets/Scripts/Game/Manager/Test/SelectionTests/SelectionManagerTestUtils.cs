﻿
using System.Collections.Generic;
using UnityEngine;
using RTSEngine.Core.Impls;
using RTSEngine.Core.Enums;
using RTSEngine.Manager.Interfaces;
using RTSEngine.Manager.Impls;

using NSubstitute;

namespace Tests.Manager
{
    public class SelectionManagerTestUtils
    {
        public static List<SelectableObjectTestStruct> GetDefaultTestListOfObjects()
        {
            return new List<SelectableObjectTestStruct>(){
                new SelectableObjectTestStruct(new Vector3(0,0,0), ObjectTypeEnum.UNIT),
                new SelectableObjectTestStruct(new Vector3(1,0,1), ObjectTypeEnum.UNIT),
                new SelectableObjectTestStruct(new Vector3(-1,0,1), ObjectTypeEnum.UNIT),
                new SelectableObjectTestStruct(new Vector3(-1,0,-1), ObjectTypeEnum.UNIT),
                new SelectableObjectTestStruct(new Vector3(2,0,3), ObjectTypeEnum.UNIT),
                new SelectableObjectTestStruct(new Vector3(4,0,3), ObjectTypeEnum.BUILDING),
                new SelectableObjectTestStruct(new Vector3(2,0,-3), ObjectTypeEnum.BUILDING),
                new SelectableObjectTestStruct(new Vector3(1,0,4), ObjectTypeEnum.ENVIRONMENT),
                new SelectableObjectTestStruct(new Vector3(4,0,-2), ObjectTypeEnum.CONSUMABLE)
            };
        }


        public static T CreateATestableObject<T>(int i) where T : SelectableObject
        {
            GameObject go = new GameObject();
            var t = go.AddComponent<T>();
            var bc = go.AddComponent<BoxCollider>();
            t.transform.position = GetDefaultTestListOfObjects()[i].pos;
            return t;
        }

        public static List<T> GetDefaultTestMainList<T>() where T : SelectableObject
        {
            List<T> mainList = new List<T>();

            for (var i = 0; i < GetDefaultTestListOfObjects().Count; i++)
            {
                var t = CreateATestableObject<T>(i);
                mainList.Add(t);
            }
            return mainList;
        }

        public static SelectionArgsXP<T, ST> GetDefaultArgs<T, ST>()
        {
            SelectionArgsXP<T, ST> args = new SelectionArgsXP<T, ST>();
            args.NewSelection = new List<T>();
            args.OldSelection = new List<T>();
            args.ToBeAdded = new List<T>();
            args.ToBeRemoved = new List<T>();
            return args;
        }

        public static T CreateGameObject<T>() where T : MonoBehaviour
        {
            var go = new GameObject();
            var so = go.AddComponent<T>();
            return so;
        }
    }
}

public struct SelectableObjectTestStruct
{
    internal Vector3 pos;
    internal ObjectTypeEnum typeEnum;

    internal SelectableObjectTestStruct(Vector3 pos, ObjectTypeEnum typeEnum)
    {
        this.pos = pos;
        this.typeEnum = typeEnum;
    }
}

