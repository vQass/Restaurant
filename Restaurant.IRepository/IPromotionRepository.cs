using Restaurant.Data.Models.PromotionModels;
using Restaurant.DB.Entities;

namespace Restaurant.IRepository
{
    public interface IPromotionRepository
    {
        long AddPromotion(PromotionCreateRequest promotionCreateRequest);
        void DeletePromotion(Promotion promotion);
        void DisablePromotion(Promotion promotion);
        void EnsurePromotionCodeNotTaken(string promotionCode, DateTime startDate, DateTime endDate, long id = 0);
        void EnsurePromotionExists(Promotion promotion);
        public void EnsurePromotionIsActive(Promotion promotion);
        void EnsurePromotionNotInUse(Promotion promotion);
        Promotion GetPromotion(long id);
        Promotion GetPromotion(string promotionCode);
        Task<IEnumerable<Promotion>> GetPromotions();
        void UpdatePromotion(Promotion promotion, PromotionUpdateRequest promotionUpdateRequest);
    }
}