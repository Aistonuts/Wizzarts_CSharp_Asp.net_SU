namespace MagicCardsmith.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MagicCardsmith.Data.Common.Repositories;
    using MagicCardsmith.Data.Models;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.SelectCategoriesViewModel;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<CardMana> cardManaRepository;
        private readonly IDeletableEntityRepository<BlackMana> blackManaRepository;
        private readonly IDeletableEntityRepository<BlueMana> blueManaRepository;
        private readonly IDeletableEntityRepository<RedMana> redManaRepository;
        private readonly IDeletableEntityRepository<WhiteMana> whiteManaRepository;
        private readonly IDeletableEntityRepository<GreenMana> greenManaRepository;
        private readonly IDeletableEntityRepository<ColorlessMana> colorlessManaRepository;
        private readonly IDeletableEntityRepository<CardFrameColor> cardFrameColorRepository;
        private readonly IDeletableEntityRepository<CardType> cardTypeRepository;
        private readonly IDeletableEntityRepository<GameExpansion> gameExpansionRepository;

        public CategoryService(
             IDeletableEntityRepository<CardMana> cardManaRepository,
             IDeletableEntityRepository<BlackMana> blackManaRepository,
             IDeletableEntityRepository<BlueMana> blueManaRepository,
             IDeletableEntityRepository<RedMana> redManaRepository,
             IDeletableEntityRepository<WhiteMana> whiteManaRepository,
             IDeletableEntityRepository<GreenMana> greenManaRepository,
             IDeletableEntityRepository<ColorlessMana> colorlessManaRepository,
             IDeletableEntityRepository<CardFrameColor> cardFrameColorRepository,
             IDeletableEntityRepository<CardType> cardTypeRepository,
             IDeletableEntityRepository<GameExpansion> gameExpansionRepository)
        {
            this.cardManaRepository = cardManaRepository;
            this.blackManaRepository = blackManaRepository;
            this.blueManaRepository = blueManaRepository;
            this.redManaRepository = redManaRepository;
            this.whiteManaRepository = whiteManaRepository;
            this.greenManaRepository = greenManaRepository;
            this.colorlessManaRepository = colorlessManaRepository;
            this.cardFrameColorRepository = cardFrameColorRepository;
            this.cardTypeRepository = cardTypeRepository;
            this.gameExpansionRepository = gameExpansionRepository;

        }

        public IEnumerable<BlackManaCostViewModel> GetAllBlackMana()
        {
            return this.blackManaRepository.AllAsNoTracking()
                .Select(x => new BlackManaCostViewModel()
                {
                    Id = x.Id,
                    Cost = x.Cost,
                })
                .OrderBy(x => x.Id)
                .ToList();
        }

        public IEnumerable<BlueManCostViewModel> GetAllBlueMana()
        {
            return this.blueManaRepository.AllAsNoTracking()
               .Select(x => new BlueManCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToList();
        }

        public IEnumerable<FrameColorViewModel> GetAllCardFrames()
        {
            return this.cardFrameColorRepository.AllAsNoTracking()
                .Select(x => new FrameColorViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                  .OrderBy(x => x.Id)
                 .ToList();
        }

        public IEnumerable<CardTypeViewModel> GetAllCardType()
        {
            return this.cardTypeRepository.AllAsNoTracking()
                .Select(x => new CardTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                  .OrderBy(x => x.Id)
                 .ToList();

        }

        public IEnumerable<ColorlessManaCostViewModel> GetAllColorlessMana()
        {
            return this.colorlessManaRepository.AllAsNoTracking()
               .Select(x => new ColorlessManaCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToList();
        }

        public IEnumerable<ExpansionInListViewModel> GetAllExpansionInListView()
        {
            return this.gameExpansionRepository.AllAsNoTracking()
               .Select(x => new ExpansionInListViewModel()
               {
                   Id = x.Id,
                   Title = x.Title,
               })
               .OrderBy(x => x.Title)
               .ToList();
        }

        public IEnumerable<GreenManaCostViewModel> GetAllGreenMana()
        {
            return this.greenManaRepository.AllAsNoTracking()
               .Select(x => new GreenManaCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToList();
        }

        public IEnumerable<RedManaCostViewModel> GetAllRedMana()
        {
            return this.redManaRepository.AllAsNoTracking()
               .Select(x => new RedManaCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToList();
        }

        public IEnumerable<WhiteManaCostViewModel> GetAllWhiteMana()
        {
            return this.whiteManaRepository.AllAsNoTracking()
              .Select(x => new WhiteManaCostViewModel()
              {
                  Id = x.Id,
                  Cost = x.Cost,
              })
              .OrderBy(x => x.Id)
              .ToList();
        }
    }
}
