using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Services
{
    public class PromotionService : IPromotionService
    {
        #region Fields

        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<PromotionService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Ctors

        public PromotionService(
            RestaurantDbContext dbContext,
            ILogger<PromotionService> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region PublicMethods

        public IEnumerable<Promotion> GetPromotionsList()
        {
            return _dbContext.Promotions.ToList();
        }

        public Promotion GetPromotionById(long id)
        {
            return CheckIfPromotionExistsById(id);
        }

        public long AddPromotion(PromotionCreateRequest promotionRequest)
        {
            promotionRequest.Code = promotionRequest.Code.Trim();
            CheckIfPromotionCodeInUse(promotionRequest.Code, promotionRequest.StartDate, promotionRequest.EndDate);

            var promotion = _mapper.Map<Promotion>(promotionRequest);

            _dbContext.Promotions.Add(promotion);
            _dbContext.SaveChanges();
            return promotion.Id;
        }

        public void UpdatePromotion(long id, PromotionUpdateRequest promotionRequest)
        {
            var promotion = CheckIfPromotionExistsById(id);

            CheckIfPromotionInUse(promotion);

            promotion.DiscountPercentage = promotionRequest.DiscountPercentage;
            promotion.StartDate = promotionRequest.StartDate;
            promotion.EndDate = promotionRequest.EndDate;
            promotion.Code = promotionRequest.Code.Trim();

            _dbContext.SaveChanges();
        }

        public void DeletePromotion(long id)
        {
            var promotion = CheckIfPromotionExistsById(id);

            CheckIfPromotionInUse(promotion);

            _dbContext.Promotions.Remove(promotion);
            _dbContext.SaveChanges();
        }

        public void DisablePromotion(long id)
        {
            var promotion = CheckIfPromotionExistsById(id);

            promotion.IsManuallyDisabled = true;

            _dbContext.SaveChanges();
        }

        #endregion

        #region PrivateMethods

        private Promotion CheckIfPromotionExistsById(long id)
        {
            var promotion = _dbContext.Promotions
                .FirstOrDefault(x => x.Id == id);

            if (promotion is null)
            {
                throw new NotFoundException("Promocja o podanym id nie istnieje.");
            }

            return promotion;
        }

        private Promotion CheckIfPromotionExistsByCode(string promotionCode)
        {
            var promotion = _dbContext.Promotions
                .FirstOrDefault(x => x.Code
                    .Equals(promotionCode, StringComparison.InvariantCultureIgnoreCase));

            if (promotion is null)
            {
                throw new NotFoundException("Promocja o podanym kodzie nie istnieje.");
            }

            return promotion;
        }

        private void CheckIfPromotionCodeInUse(string promotionCode, DateTime startDate, DateTime endDate, int id = 0)
        {
            var promotionInUse = _dbContext.Promotions
                .Where(x => x.Id != id)
                .Where(x => x.StartDate < startDate && x.EndDate > endDate)
                .Any(x => x.Code
                    .Equals(promotionCode, StringComparison.InvariantCultureIgnoreCase));

            if (promotionInUse)
            {
                _logger.LogError($"Name of promotion {promotionCode} is taken in given period of time ({startDate} - {endDate}).");
                throw new NotFoundException("Podana nazwa promocji jest zajęta.");
            }
        }

        private void CheckIfPromotionInUse(Promotion promotion)
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
