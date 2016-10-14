namespace JoinAndDo.Entities
{
    public abstract class JoinsEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }

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
            Title = "";
            Text = "";
            _people = 0;
            _allPeople = 1;
        }
    }
}
