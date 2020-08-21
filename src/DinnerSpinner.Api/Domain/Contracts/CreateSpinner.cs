namespace DinnerSpinner.Api.Domain.Contracts
{
    public class CreateSpinner
    {
        public string Name { get; set; }

        public string OwnerEmail { get; set; }

        public string OwnerName { get; set; }
    }
}
