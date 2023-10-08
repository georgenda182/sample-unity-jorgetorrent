namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public class DialogWithEnemy : DialogsDispatcher
    {
        protected override void DefineDialogs()
        {
            _dialogs.Enqueue(new DialogWithText("There you are, you vermin."));
            _dialogs.Enqueue(new DialogWithText("After that I got osteoarthritis and I was never the same again."));
            _dialogs.Enqueue(new DialogWithText("And all because of you."));
            _dialogs.Enqueue(new DialogWithText("I'll never forgive you for taking the milk out of your grocery store."));
            _dialogs.Enqueue(new DialogWithText("Prepare to die, you bastard."));
        }
    }
}