namespace JoinAndDo.Entities
{
    public class MyAccession : JoinsEntity
    {
        public bool IsComplete { get; set; }

        public MyAccession()
        {
            Title = "";
            Text = "";
            People = 0;
            AllPeople = 0;
        }
    }
}
