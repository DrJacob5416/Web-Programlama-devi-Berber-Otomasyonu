using Berber.Models.Tables;
using Microsoft.AspNetCore.Identity;

namespace Berber.Areas.Admin.Models
{
    public class UserRoleViewModel
    {
        public List<UserListModel> Users { get; set; }
        public List<ApplicationRole> Roles { get; set; }
    }
}
