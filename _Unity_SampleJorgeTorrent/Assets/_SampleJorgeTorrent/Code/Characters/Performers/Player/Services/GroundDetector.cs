namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Services
{
    public interface GroundDetector
    {
        public delegate void GroundCallback();

        public event GroundCallback OnJustGrounded;
        public event GroundCallback OnJustUngrounded;
    }
}