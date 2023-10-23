namespace _SampleJorgeTorrent.Code.LoadingProcess
{
    public interface LoadingProgressVisualizer
    {
        public delegate void LoadingProgressVisualizerCallback();

        public event LoadingProgressVisualizerCallback OnShown;
        public event LoadingProgressVisualizerCallback OnHidden;

        void Show();
        void Hide();
        void SetProgress(float progress);
    }
}