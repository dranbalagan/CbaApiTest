namespace CbaApiTest.Models
{
    public class PostNewPetRequestAndResponseModel
    {
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public int id { get; set; }
        public Category category { get; set; }
        public List<Tag> tags { get; set; }
        public string status { get; set; }

        public class Category
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Tag
        {
            public object id { get; set; }
            public string name { get; set; }
        }
    }
}
