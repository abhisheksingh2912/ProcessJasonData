using ProcessJasonData.Model;

namespace ProcessJasonData.Common
{
    interface ITextManipulator
    {
        void CopyToClipBoard(JasonData data);
    }
}
