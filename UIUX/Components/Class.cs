using UIUX.Components.Pages;
using static UIUX.Components.MeetingService;

namespace UIUX.Components
{
    public class LoginService
    {
        public string name { get; set; }
        private bool isLoggedIn = false;
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set
            {
                if (value != isLoggedIn)
                {
                    isLoggedIn = value;
                    OnLoginStateChanged?.Invoke();
                }
            }
        }
        public string Login { get; set; } = "Login";

        public event Action OnLoginStateChanged;

    }

    public class MeetingService
    {
        public record Meeting(string StudentName, DateTime MeetingDateTime, string Location);

        public List<Meeting> meetings { get; set; } = new List<Meeting>();

        public DateTime currentDateTime { get => DateTime.Now; }

        public string StudentPageMeetingParam { get; set; }

        public void SetBaseMeetings()
        {
            if (meetings.Count == 0)
            {
                Meeting meeting1 = new Meeting("Jack Jackson", new DateTime(2024, 4, 29, 10, 0, 0), "RBB-217");
                Meeting meeting2 = new Meeting("John Johnson", new DateTime(2024, 4, 29, 11, 0, 0), "RBB-217");
                Meeting meeting3 = new Meeting("George Georgeson", new DateTime(2024, 4, 30, 10, 0, 0), "RBB-217");
                meetings.Add(meeting3);
                meetings.Add(meeting2);
                meetings.Add(meeting1);
            }
        }

        public void AddMeeting(Meeting meeting)
        {
            bool canAddMeeting = true;
            if (meetings.Count == 0)
            {
                meetings.Add(meeting);
            }
            else
            {
                foreach (Meeting meet in meetings)
                {
                    if (meet.MeetingDateTime == meeting.MeetingDateTime)
                    {
                        canAddMeeting = false; break;
                    }
                }
                if (canAddMeeting)
                {
                    meetings.Add(meeting);
                }
            }
        }

        public void SortMeetings()
        {
            for (int i = 0; i < meetings.Count - 1; i++)
            {
                for (int j = i + 1; j < meetings.Count; j++)
                {
                    if (meetings[i].MeetingDateTime > meetings[j].MeetingDateTime)
                    {
                        Meeting temp = meetings[i];
                        meetings[i] = meetings[j];
                        meetings[j] = temp;
                    }
                }
            }
            for (int i = meetings.Count - 1; i >= 0; i--)
            {
                if (meetings[i].MeetingDateTime < DateTime.Now)
                {
                    meetings.RemoveAt(i);
                }
            }
        }
    }

    public class StudentService
    {
        public record Student(string StudentName, float AverageReflection, int MostRecentReflection, int YearOfStudy);

        public List<Student> students { get; set; } = new List<Student>();
        public List<Student> highRiskStudents { get; set; } = new List<Student>();

        public void SetBaseStudents()
        {
            
            Student student1 = new Student("Jack Jackson", 4.5f, 5, 1);
            Student student2 = new Student("John Johnson", 3.7f, 3, 2);
            Student student3 = new Student("George Georgeson", 3.6f, 1, 3);
            students.Add(student1);
            students.Add(student2);
            students.Add(student3);
            foreach (Student student in students)
            {
                if (student.AverageReflection <= 2.5 || student.MostRecentReflection <= 2.5)
                {
                    highRiskStudents.Add(student);
                }
            }
        }
    }

    public class AppState
    {
        public event Action OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
