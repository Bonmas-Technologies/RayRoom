namespace RayRoom.Core
{
    public static class Utils
    {
        public static float VolumeToDB(float volume)
        {
            return 20 * MathF.Log10(volume);
        }
        
        public static float DBToVolume(float db)
        {
            return MathF.Pow(10f, db / 20f);
        }
    }
}
