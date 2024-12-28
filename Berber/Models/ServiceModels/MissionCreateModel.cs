using System.ComponentModel.DataAnnotations;

namespace Berber.Models.ServiceModels
{
    public class MissionCreateModel
    {
        [Display(Name = "Hizmet İsmi")]
        public string Name { get; set; }
    }
}
