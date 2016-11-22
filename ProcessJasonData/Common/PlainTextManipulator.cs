using ProcessJasonData.Model;
using System.Windows;

namespace ProcessJasonData.Common
{
    class PlainTextManipulator: ITextManipulator
    {
        public void CopyToClipBoard(JasonData data)
        {
            if (data != null)
            {
                Clipboard.SetText(data.Body);
            }
        }
    }
}
