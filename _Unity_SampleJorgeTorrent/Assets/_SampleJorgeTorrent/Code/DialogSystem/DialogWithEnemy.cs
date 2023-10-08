namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public class DialogWithEnemy : DialogsDispatcher
    {
        protected override void DefineDialogs()
        {
            _dialogs.Add(new DialogWithText("There you are, you vermin."));
            _dialogs.Add(new DialogWithText("After that I got osteoarthritis and I was never the same again."));
            _dialogs.Add(new DialogWithText("And all because of you."));
            _dialogs.Add(new DialogWithText("I'll never forgive you for taking the milk out of your grocery store."));
            _dialogs.Add(new DialogWithText("Prepare to die, you bastard."));
        }
    }
}