using UnityEngine;

namespace _SampleJorgeTorrent.Code.DialogSystem
{
    public class EnemyDialoguesDispatcher : DialogsDispatcher
    {
        [SerializeField] private string[] _texts;

        protected override void DefineDialogs()
        {
            foreach (string text in _texts)
            {
                AddDialog(new TextDialog(text));
            }
            StartDialogs();
        }
    }
}