using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;

namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public class DialogWithText : Dialog
    {
        private DialogSpeechBubble _speechBubble;
        private string _text;

        public DialogWithText(string text)
        {
            _text = text;
        }

        public override void Install(ServiceLocator serviceLocator)
        {
            _speechBubble = serviceLocator.GetService<DialogSpeechBubble>();
        }

        public override void Show()
        {
            _speechBubble.DisplayText(_text);
        }

        public override void Close()
        {
            _speechBubble.ForceFinish();
        }
    }
}