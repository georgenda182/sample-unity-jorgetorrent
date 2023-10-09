namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public class DialogWithEnemy : DialogsDispatcher
    {
        protected override void DefineDialogs()
        {
            AddDialog(new DialogWithText("There you are, you vermin."));
            AddDialog(new DialogWithText("After that I got osteoarthritis and I was never the same again."));
            AddDialog(new DialogWithText("And all because of you."));
            AddDialog(new DialogWithText("I'll never forgive you for taking the milk out of your grocery store."));
            AddDialog(new DialogWithText("Prepare to die, you bastard."));
            StartDialogs();
        }
    }
}