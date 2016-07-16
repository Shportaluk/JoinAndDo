namespace JoinAndDo.Entities
{
    public class MyAccession : JoinsEntity
    {
        public bool isComplete;

        public MyAccession()
        {
            title = "";
            text = "";
            People = 0;
            AllPeople = 0;
        }
    }
}
