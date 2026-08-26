//the idea of this project is initially a web api simulating a website to study, with notes, flashcards etc

//We would want a simple register first: user, email and password; after logging in we'd like Subject separating the notes

using System.ComponentModel.DataAnnotations;

public class User
{

    [Required, Length(4, 16)]
    public string? Username {get; set;}

    [Required, Length(8, 50)]
    public string? Email { get; set;}

    [Required, Length(8, 30)]
    public string? Password {get; set;} //hidden

    public int UserId {get; set;}

}

public class Study
{
    public int StudyId {get; set;}
    List<Study> notes = new List<Study>();

    public string? Note {get; set;}

    [Required, Length(4, 16)]
    public string? Subject {get; set;}

    
}