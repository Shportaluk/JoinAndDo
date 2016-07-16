namespace JoinAndDo.Entities
{
    public abstract class JoinsEntity
    {
        public string title;
        public string text;

        private int _people;
        private int _allPeople;

        public int People
        {
            get
            {
                return _people;
            }
            set
            {
                if( value >= 0 )
                {
                    _people = value;
                }
                else
                {
                    _people = 0;
                }
            }
        }
        public int AllPeople
        {
            get
            {
                return _allPeople;
            }
            set
            {
                if( value > 0 )
                {
                    _allPeople = value;
                }
                else
                {
                    _allPeople = 1;
                }
            }
        }


        public JoinsEntity(  )
        {
            title = "";
            text = "";
            _people = 0;
            _allPeople = 1;
        }
    }
}
