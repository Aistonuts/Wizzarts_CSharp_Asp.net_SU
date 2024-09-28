namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
    using Wizzarts.Services.Mapping;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard.PlayCardComponents;

    public class PlayCardComponentsService : IPlayCardComponentsService
    {
        private readonly IDeletableEntityRepository<ManaInCard> cardManaRepository;
        private readonly IDeletableEntityRepository<BlackMana> blackManaRepository;
        private readonly IDeletableEntityRepository<BlueMana> blueManaRepository;
        private readonly IDeletableEntityRepository<RedMana> redManaRepository;
        private readonly IDeletableEntityRepository<WhiteMana> whiteManaRepository;
        private readonly IDeletableEntityRepository<GreenMana> greenManaRepository;
        private readonly IDeletableEntityRepository<ColorlessMana> colorlessManaRepository;
        private readonly IDeletableEntityRepository<PlayCardFrameColor> cardFrameColorRepository;
        private readonly IDeletableEntityRepository<PlayCardType> cardTypeRepository;
        private readonly IDeletableEntityRepository<CardGameExpansion> cardGameExpansionRepository;

        public PlayCardComponentsService(
             IDeletableEntityRepository<ManaInCard> cardManaRepository,
             IDeletableEntityRepository<BlackMana> blackManaRepository,
             IDeletableEntityRepository<BlueMana> blueManaRepository,
             IDeletableEntityRepository<RedMana> redManaRepository,
             IDeletableEntityRepository<WhiteMana> whiteManaRepository,
             IDeletableEntityRepository<GreenMana> greenManaRepository,
             IDeletableEntityRepository<ColorlessMana> colorlessManaRepository,
             IDeletableEntityRepository<PlayCardFrameColor> cardFrameColorRepository,
             IDeletableEntityRepository<PlayCardType> cardTypeRepository,
             IDeletableEntityRepository<CardGameExpansion> cardGameExpansionRepository)
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
            this.cardGameExpansionRepository = cardGameExpansionRepository;
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
            return this.cardGameExpansionRepository.AllAsNoTracking()
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
