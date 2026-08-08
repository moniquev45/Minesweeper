namespace Minesweeper
{
    //Sets up time and its visual, and makes changes when it ticks to the next second.
    public class Clock
    {
        private Counter _seconds;
        private Counter _minutes;
        private Counter _hours;

        //Sets up what time contains.
        public Clock()
        {
            _seconds = new Counter(0, "seconds", 59);
            _minutes = new Counter(0, "minutes", 59);
            _hours = new Counter(0, "hours", 12);
        }

        //Ticks, changes the time when the max reached time for that section has been reached.
        public void Tick()
        {
            _seconds.Increment();
            if (_seconds.HaveYouReachedMax == true)
            {
                _minutes.Increment();
                if (_minutes.HaveYouReachedMax == true)
                {
                    _hours.Increment();
                    if (_hours.HaveYouReachedMax == true)
                    {
                        Reset();
                    }
                }
            }
        }

        public void Reset()
        {
            _seconds.Reset();
            _minutes.Reset();
            _hours.Reset();
        }

        public string ClockDisplay()
        {
            return $"{_hours.Count:D2}:{_minutes.Count:D2}:{_seconds.Count:D2}";
        }
    }
}
