using AutoMapper;


namespace Restaurant.API.AutoMapper
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            //CreateMap<ArticleCreateDto, Article>();
            //CreateMap<Article, ArticleDisplayDto>();

            //CreateMap<ArticleContent, ArticleContentDisplayDto>().ForMember(x => x.ArticleName, c => c.MapFrom(s => s.Article.Name));
        }
    }
}
