using Models.Utils.I18N;
using System.ComponentModel.DataAnnotations;

namespace BaseAndApiDocker.Controllers
{
    public class ListRequest
    {
        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        public DateTimeOffset? StartDate { get; init; } = DateTimeOffset.Now.AddDays(-30);

        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        public DateTimeOffset? EndDate { get; init; } = DateTimeOffset.Now;
    }
}