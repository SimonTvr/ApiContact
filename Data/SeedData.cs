public class SeedData {
    public void Init() {
        using(var context=new ApiContactContext())
        {
            // Todo FaireVaisselle = new Todo
            // {
            //     Task = "FaireVaisselle",
            //     Completed = true,
            //     Deadline = DateTime.Parse("2023-10-02"),
            // };
            // Todo FaireMenage = new Todo
            // {
            //     Task = "FaireMenage",
            //     Completed = false,
            //     Deadline = DateTime.Parse("2023-11-17"),
            // };
            // Todo FaireSport = new Todo
            // {
            //     Task = "FaireSport",
            //     Completed = false,
            //     Deadline = DateTime.Parse("2023-12-25"),
            // };
            // context.ApiTodo.AddRange(
            //     FaireVaisselle,
            //     FaireMenage,
            //     FaireSport
            // );
            // context.SaveChanges();
        }
    }
}
