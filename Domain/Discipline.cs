namespace UescCoursesAPI.Domain;

public class Discipline
{
   public int DisciplineId { get; set; }
   public string Title { get; set; }
   public string? Syllabus { get; set; }
   public int Workload { get; set; }
   public virtual ICollection<Curriculum>? Curriculums { get; set; }

}
