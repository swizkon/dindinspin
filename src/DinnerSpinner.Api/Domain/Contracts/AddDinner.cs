namespace DinnerSpinner.Api.Domain.Contracts
{
    public class AddDinner
    {
        public string SpinnerId { get; set; }

        public string Name { get; set; }

        public string[] Ingredients { get; set; }
    }
}