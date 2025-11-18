using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOS.BasketDTOs
{
  public record BasketItemDTO(
      
      string Id,
      string ProductName,
      [Range(1,double.MaxValue)]
      decimal Price,
        [Range(1,100)]
      int Quantity,
      string PictureUrl);
}
