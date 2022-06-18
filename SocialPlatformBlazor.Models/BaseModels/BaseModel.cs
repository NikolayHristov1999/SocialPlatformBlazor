using System.ComponentModel.DataAnnotations;

namespace SocialPlatformBlazor.Models.BaseModels
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        public BaseModel()
        {
            CreatedOn = DateTime.UtcNow;
        }
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
