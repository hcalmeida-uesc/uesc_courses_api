namespace UescCoursesAPI.Domain;

public class Curriculum

{
   public int CurriculumId { get; set; }
   public int Workload { get; set; }
   public int Year { get; set; }
   public string? Status { get; set; }
   public int PedagogicProjectId { get; set; }
   public virtual PedagogicProject? PedagogicProject { get; set; }
   public virtual ICollection<Discipline>? Disciplines { get; set; }
}
