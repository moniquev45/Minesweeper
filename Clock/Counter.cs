namespace Minesweeper
{
    //Counts incrementally to the max.
    public class Counter
    {
        private int _count;
        private string _name;
        private int _maxValue;

        private bool _haveYouReachedMax;

        public Counter(int count, string name, int maxValue)
        {
            _count = count;
            _name = name;
            _maxValue = maxValue;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
        }

        public bool HaveYouReachedMax
        {
            get
            {
                return _haveYouReachedMax;
            }
        }

        //Increase the number of the count every time its incremented.
        public void Increment()
        {
            _count++;
            if (_count > _maxValue)
            {
                _count = 0;
                _haveYouReachedMax = true;
            }
            else
            {
                _haveYouReachedMax = false;
            }
        }

        public void Reset()
        {
            _count = 0;
        }
    }
}
