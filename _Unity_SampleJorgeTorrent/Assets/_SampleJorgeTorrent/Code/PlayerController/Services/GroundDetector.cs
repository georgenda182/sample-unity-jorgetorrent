namespace _SampleJorgeTorrent.Code.PlayerController.Services
{
    public interface GroundDetector
    {
        public delegate void GroundCallback();

        public event GroundCallback OnJustGrounded;
        public event GroundCallback OnJustUngrounded;
    }
}