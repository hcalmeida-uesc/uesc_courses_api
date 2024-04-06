

namespace UescCoursesAPI.Domain;

public class PedagogicProject
{
   public int PedagogicProjectId { get; set; }
   public required int Year { get; set; }
   public required string Status { get; set; }
   public int CourseId { get; set; }
   public virtual Curriculum? Curriculum { get; set; }
   public virtual Course? Course { get; set; }
}
