using System.ComponentModel.DataAnnotations;

namespace CommandService.Dto
{
    public class CommandCreatedDto
    {
       
       [Required]
       
        public string HowTo { get; set; }
      
        [Required]
        public string CommandLine { get; set; }
       
       
        
    }
}