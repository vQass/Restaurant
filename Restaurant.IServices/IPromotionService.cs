using Restaurant.Data.Models.PromotionModels;
using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices
{
    public interface IPromotionService
    {
        public Task<IEnumerable<Promotion>> GetPromotionsList();
        public Promotion GetPromotionById(long id);
        public long AddPromotion(PromotionCreateRequest promotion);
        public void UpdatePromotion(long id, PromotionUpdateRequest promotion);
        public void DeletePromotion(long id);
        public void DisablePromotion(long id);
    }
}
