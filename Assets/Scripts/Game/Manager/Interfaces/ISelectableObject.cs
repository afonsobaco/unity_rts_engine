using RTSEngine.Core;
using RTSEngine.Utils;

namespace RTSEngine.Manager
{
    public interface ISelectableObject : ISelectable
    {
        SelectionMark SelectionMark { get; set; }
        SelectionMark PreSelectionMark { get; set; }
        GUISelectableObjectInfoSO SelectableObjectInfo { get; set; }
        bool IsCompatible(ISelectable other);


    }



}