using System.ComponentModel.DataAnnotations;

public class BookFormat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Token { get; set; }
}

public class BookFormatViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    public string Description { get; set; }
}