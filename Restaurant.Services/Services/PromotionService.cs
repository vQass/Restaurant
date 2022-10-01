using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IRepository;
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
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;

        public PromotionService(
            IPromotionRepository promotionRepository,
            IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<Promotion>> GetPromotionsList()
        {
            return await _promotionRepository.GetPromotions();
        }

        public Promotion GetPromotionById(long id)
        {
            var promotion = _promotionRepository.GetPromotion(id);

            _promotionRepository.EnsurePromotionExists(promotion);

            return promotion;
        }

        public long AddPromotion(PromotionCreateRequest promotionRequest)
        {
            _promotionRepository.EnsurePromotionCodeNotTaken(
                promotionRequest.Code,
                promotionRequest.StartDate,
                promotionRequest.EndDate);

            var id = _promotionRepository.AddPromotion(promotionRequest);

            return id;
        }

        public void UpdatePromotion(long id, PromotionUpdateRequest promotionRequest)
        {
            var promotion = _promotionRepository.GetPromotion(id);

            _promotionRepository.EnsurePromotionExists(promotion);

            _promotionRepository.EnsurePromotionCodeNotTaken(
                promotionRequest.Code,
                promotionRequest.StartDate,
                promotionRequest.EndDate,
                id);

            _promotionRepository.EnsurePromotionNotInUse(promotion);

            _promotionRepository.UpdatePromotion(promotion, promotionRequest);
        }

        public void DeletePromotion(long id)
        {
            var promotion = _promotionRepository.GetPromotion(id);

            _promotionRepository.EnsurePromotionExists(promotion);

            _promotionRepository.EnsurePromotionNotInUse(promotion);

            _promotionRepository.DeletePromotion(promotion);
        }

        public void DisablePromotion(long id)
        {
            var promotion = _promotionRepository.GetPromotion(id);

            _promotionRepository.EnsurePromotionExists(promotion);

            _promotionRepository.DeletePromotion(promotion);
        }
    }
}
