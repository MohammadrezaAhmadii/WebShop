using Common.Dto;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Products.Commands
{
    public interface IAddNewProduct
    {
        ResultDto ExecutResult(ProductDto product);
    }
}
