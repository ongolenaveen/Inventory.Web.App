using System.Web.Mvc;

namespace Inventory.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController:BaseController
    {
    }
}
