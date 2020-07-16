using System;

namespace Core
{
    public class Coordenate
    {
        public double x { get; private set; }
        public double y { get; private set; }

        public Coordenate(double x, double y)
        {
            if (!isValidCoordinate(x, y)) new Exception("It doesn't seen to be a valid geografic coordinate");

            this.x = x;
            this.y = y;
        }

        ////This is a bonus function, it's unused, to reject invalid lat/longs.
        private bool isValidCoordinate(double latitude, double longitude) => (latitude > -90 && latitude < 90 && longitude > -180 && longitude < 180) ? true : false;

    }
}
