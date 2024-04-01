namespace UescCoursesAPI.Domain;

public class Course
{
   public int CourseId { get; set; }
   public required string Name { get; set; }
   public required string Status { get; set; }
}
