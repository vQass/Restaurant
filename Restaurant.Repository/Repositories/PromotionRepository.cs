using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB.Entities;
using Restaurant.DB;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.IRepository;
using AutoMapper;

namespace Restaurant.Repository.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<PromotionRepository> _logger;
        private readonly IMapper _mapper;

        public PromotionRepository(RestaurantDbContext dbContext,
            ILogger<PromotionRepository> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        #region GetMethods

        public Promotion GetPromotion(long id)
        {
            return _dbContext.Promotions.FirstOrDefault(x => x.Id == id);
        }

        public Promotion GetPromotion(string promotionCode)
        {
            return _dbContext.Promotions
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefault(x => x.Code
                    .Equals(promotionCode));
        }

        public async Task<IEnumerable<Promotion>> GetPromotions()
        {
            return await _dbContext.Promotions
                .AsNoTracking()
                .ToListAsync();
        }

        #endregion

        #region EntityModificationMethods

        public long AddPromotion(PromotionCreateRequest promotionCreateRequest)
        {
            var promotion = _mapper.Map<Promotion>(promotionCreateRequest);

            _dbContext.Promotions.Add(promotion);
            _dbContext.SaveChanges();

            return promotion.Id;
        }

        public void UpdatePromotion(Promotion promotion, PromotionUpdateRequest promotionUpdateRequest)
        {
            promotion.Code = promotionUpdateRequest.Code;
            promotion.DiscountPercentage = promotionUpdateRequest.DiscountPercentage;
            promotion.StartDate = promotionUpdateRequest.StartDate;
            promotion.EndDate = promotionUpdateRequest.EndDate;

            _dbContext.SaveChanges();
        }

        public void DeletePromotion(Promotion promotion)
        {
            _dbContext.Promotions.Remove(promotion);
            _dbContext.SaveChanges();
        }

        public void DisablePromotion(Promotion promotion)
        {
            promotion.IsManuallyDisabled = true;
            _dbContext.SaveChanges();
        }

        #endregion

        #region ValidationMethods

        public void EnsurePromotionExists(Promotion promotion)
        {
            if (promotion is null)
            {
                throw new NotFoundException("Podana promocja nie istnieje.");
            }
        }

        public void EnsurePromotionIsActive(Promotion promotion)
        {
            var currentDateTime = DateTime.Now;

            if (promotion.IsManuallyDisabled)
            {
                throw new BadRequestException("Podana promocja została wyłączona przez administratora.");
            }

            if(promotion.StartDate > currentDateTime || promotion.EndDate < currentDateTime)
            {
                throw new BadRequestException("Podana promocja nie jest już aktywna.");
            }
        }

        public void EnsurePromotionCodeNotTaken(
            string promotionCode,
            DateTime startDate,
            DateTime endDate,
            long id = 0)
        {
            var cityNameInUse = _dbContext.Promotions
                .Where(x => x.Id != id)
                .Where(x => x.StartDate < startDate && x.EndDate > endDate)
                .Any(x => x.Code
                    .ToLower()
                    .Replace(" ", "")
                    .Equals(
                        promotionCode
                            .ToLower()
                            .Replace(" ", "")));

            if (cityNameInUse)
            {
                _logger.LogError($"Code: {promotionCode} is taken.");
                throw new BadRequestException("Podany kod promocji jest zajęty.");
            }
        }

        public void EnsurePromotionNotInUse(Promotion promotion)
        {
            var promotionInUse = _dbContext.Orders
                .Any(x => x.Promotion.Id == promotion.Id);

            if (promotionInUse)
            {
                _logger.LogError($"Promotion with code {promotion.Code} is used in orders table.");
                throw new BadRequestException("Podana promocja używana jest w co najmniej jednym zamówieniu.");
            }
        }

        #endregion
    }
}
