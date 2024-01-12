namespace TrainingStudio02.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public ICollection<FitnessClass>? FitnessClasses { get; set; }

    }
}
