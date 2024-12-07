namespace Wizzarts.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Wizzarts.Data.Common.Repositories;
    using Wizzarts.Data.Models;
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

        public Task<bool> BlackManaExistsAsync(int id)
        {
            return this.blackManaRepository.AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        public Task<bool> BlueManaExistsAsync(int id)
        {
            return this.blueManaRepository.AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        public Task<bool> CardFrameExistsAsync(int id)
        {
            return this.cardFrameColorRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public Task<bool> CardTypeExistsAsync(int id)
        {
            return this.cardTypeRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public Task<bool> ColorlessManaExistsAsync(int id)
        {
            return this.colorlessManaRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public Task<bool> ExpansionExistsAsync(int id)
        {
            return this.cardGameExpansionRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<BlackManaCostViewModel>> GetAllBlackMana()
        {
            return await this.blackManaRepository.AllAsNoTracking()
                .Select(x => new BlackManaCostViewModel()
                {
                    Id = x.Id,
                    Cost = x.Cost,
                })
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlueManCostViewModel>> GetAllBlueMana()
        {
            return await this.blueManaRepository.AllAsNoTracking()
               .Select(x => new BlueManCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToListAsync();
        }

        public async Task<IEnumerable<FrameColorViewModel>> GetAllCardFrames()
        {
            return await this.cardFrameColorRepository.AllAsNoTracking()
                .Select(x => new FrameColorViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                  .OrderBy(x => x.Id)
                 .ToListAsync();
        }

        public async Task<IEnumerable<CardTypeViewModel>> GetAllCardType()
        {
            return await this.cardTypeRepository.AllAsNoTracking()
                .Select(x => new CardTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                  .OrderBy(x => x.Id)
                 .ToListAsync();
        }

        public async Task<IEnumerable<ColorlessManaCostViewModel>> GetAllColorlessMana()
        {
            return await this.colorlessManaRepository.AllAsNoTracking()
               .Select(x => new ColorlessManaCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToListAsync();
        }

        public async Task<IEnumerable<ExpansionInListViewModel>> GetAllExpansionInListView()
        {
            return await this.cardGameExpansionRepository.AllAsNoTracking()
               .Select(x => new ExpansionInListViewModel()
               {
                   Id = x.Id,
                   Title = x.Title,
               })
               .OrderBy(x => x.Title)
               .ToListAsync();
        }

        public async Task<IEnumerable<GreenManaCostViewModel>> GetAllGreenMana()
        {
            return await this.greenManaRepository.AllAsNoTracking()
               .Select(x => new GreenManaCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToListAsync();
        }

        public async Task<IEnumerable<RedManaCostViewModel>> GetAllRedMana()
        {
            return await this.redManaRepository.AllAsNoTracking()
               .Select(x => new RedManaCostViewModel()
               {
                   Id = x.Id,
                   Cost = x.Cost,
               })
               .OrderBy(x => x.Id)
               .ToListAsync();
        }

        public async Task<IEnumerable<WhiteManaCostViewModel>> GetAllWhiteMana()
        {
            return await this.whiteManaRepository.AllAsNoTracking()
              .Select(x => new WhiteManaCostViewModel()
              {
                  Id = x.Id,
                  Cost = x.Cost,
              })
              .OrderBy(x => x.Id)
              .ToListAsync();
        }

        public Task<bool> GreenManaExistsAsync(int id)
        {
            return this.greenManaRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public Task<bool> RedManaExistsAsync(int id)
        {
            return this.redManaRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }

        public Task<bool> WhiteManaExistsAsync(int id)
        {
            return this.whiteManaRepository.AllAsNoTracking().AnyAsync(x => x.Id == id);
        }
    }
}
